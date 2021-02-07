using EmployeeData.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeData.SeedData
{
    public static class DataSeeding
    {
        public static void EmployeeDataSeeding(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                Id = 2,
                Name = "morshed sarwer",
                Email = "morshedsarwer@email.com",
                
            });
        }
    }
}