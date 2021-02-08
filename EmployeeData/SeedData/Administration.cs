using System.Collections.Generic;
using EmployeeData.Context;
using EmployeeData.Interfaces;
using EmployeeData.POCO;
using EmployeeData.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace EmployeeData.SeedData
{
    public class Administration : IAdministration
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public Administration(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public RoleAssignViewModels RoleAssignViewModelsEnumerable()
        {
            var role = _roleManager.Roles;
            if (role == null) return null;
            var users = _userManager.Users;
            if (users == null) return null;
            var data = new RoleAssignViewModels
            {
                Roles = role,
                Users = users
            };
            
            return data;

        }
    }
}