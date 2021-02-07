using System.Collections.Generic;
using EmployeeData.Models;

namespace EmployeeData.Interfaces
{
    public interface IEmployee
    {
        Employee Add(Employee employee);
        IEnumerable<Employee> Employees();
        Employee Delete(int id);
        Employee Update(Employee employee);
    }
}