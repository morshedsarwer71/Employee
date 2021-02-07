using EmployeeData.Models;
using EmployeeData.SeedData;
using Microsoft.EntityFrameworkCore;

namespace EmployeeData.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.EmployeeDataSeeding();
          
        }
    }
}