using System.Collections.Generic;
using EmployeeData.Context;
using EmployeeData.Interfaces;
using EmployeeData.Models;

namespace EmployeeData.Services
{
    public class EmployeeRepository : IEmployee
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }
        public Employee Add(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return employee;
        }

        public IEnumerable<Employee> Employees()
        {
            return _context.Employees;
        }

        public Employee Delete(int id)
        {
            var emp = _context.Employees.Find(id);
            _context.Employees.Remove(emp);
            _context.SaveChanges();
            return emp;
        }

        public Employee Update(Employee employee)
        {
            var emp = _context.Employees.Attach(employee);
            emp.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return employee;
        }
    }
}