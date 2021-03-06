using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeData.Context;
using EmployeeData.Interfaces;
using EmployeeData.SeedData;
using EmployeeData.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Employee.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>(option=>option.UseNpgsql(Configuration.GetConnectionString("ConnectionString"),b=>
                b.MigrationsAssembly("Employee.Web")));
            services.AddScoped<IEmployee, EmployeeRepository>();
            services.AddScoped<IAdministration, Administration>();
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    //this change PassOptions Class from IdentityOption Class
                    options.Password.RequiredLength = 5;
                    options.Password.RequiredUniqueChars = 1;
                }).AddEntityFrameworkStores<AppDbContext>();
            services.AddControllers();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
