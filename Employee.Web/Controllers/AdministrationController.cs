using System.Threading.Tasks;
using EmployeeData.Context;
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

        public AdministrationController(RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
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
    }
}