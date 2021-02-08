using System.Threading.Tasks;
using EmployeeData.Context;
using EmployeeData.Interfaces;
using EmployeeData.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Employee.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdministrationController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAdministration _administration;

        public AdministrationController
            (
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            IAdministration administration
            )
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _administration = administration;
        }
        
        [HttpPost]
        [Route("role")]
        public async Task<IActionResult> CreateRole(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole
                {
                    Name = model.Name
                };

                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return Ok(model);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }

            return Ok(model);
        }
        [HttpGet]
        [Route("Roles")]
        public IActionResult GetRoles()
        {
            return Ok(_roleManager.Roles);
        }

        [HttpGet]
        [Route("RoleWithUser")]
        public async Task<IActionResult> GetRoleAndUsersById(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return Ok("Role not found");
            }

            var editRole = new EditRoleViewModel
            {
                Id = role.Id,
                EditRoleName = role.Name
            };

            var users = _userManager.Users;

            foreach (var user in users)
            {
                if(await _userManager.IsInRoleAsync(user, role.Name))
                    editRole.Users.Add(user.UserName);
            }

            return Ok(editRole);

        }
        [HttpGet]
        [Route("roleandusers")]
        public IActionResult RoleAndUsers()
        {
            var data = _administration.RoleAssignViewModelsEnumerable();
            if (data!=null)
            {
                return Ok(data);
            }

            return Ok("not found");
        }
        
        [HttpPost]
        [Route("addrole")]
        public async Task<IActionResult> AddUserToRole(AddUserToRoleModels roleModels)
        {
            var user = await _userManager.FindByIdAsync(roleModels.UserId);
            var role = await _roleManager.FindByIdAsync(roleModels.RoleId);
            if (user != null && role != null)
            {
                var result = await _userManager.AddToRoleAsync(user, role.Name);
                if (result.Succeeded)
                {
                    return Ok(result.Succeeded);
                }
            }

            return Ok("user or role not found");
        }
    }
}