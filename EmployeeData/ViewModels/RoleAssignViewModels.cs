using System.Collections.Generic;
using EmployeeData.Context;
using EmployeeData.POCO;
using Microsoft.AspNetCore.Identity;

namespace EmployeeData.ViewModels
{
    public class RoleAssignViewModels
    {
        public IEnumerable<IdentityRole> Roles { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }
    }
}