using System.Linq;
using System.Threading.Tasks;
using EmployeeData.Context;
using EmployeeData.ViewModels;
using EmployeeData.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Employee.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        // this method use for core mvc as remote validation not for core web api
        [AcceptVerbs("Get","Post")]
        public async Task<IActionResult> IsEmailUse(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user==null ? Json(true) : Json($"your email id {email} already used");
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
           
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    City = model.City,
                    Gender = model.Gender
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                return result.Succeeded ? Ok(user) : Ok("not inserted");
        }
        [HttpPost]
        [Route("LogOut")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("Logout");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            var user =await _signInManager.PasswordSignInAsync(login.UserName, login.Password, false, false);
            return user.Succeeded ? Ok("sign in") : Ok("invalid login");
        }
        [HttpGet]
        [Route("users")]
        public  IActionResult Users()
        {
            var data = _userManager.Users;
            return Ok(data);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("user")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user!=null)
            {
                var claims = await _userManager.GetClaimsAsync(user);
                var roles = await _userManager.GetRolesAsync(user);

                var userViewModels = new UserViewModels
                {
                    Id = user.Id,
                    Name = user.UserName,
                    Email = user.Email,
                    Claims = claims.Select(x=>x.Value).ToList(),
                    Roles = roles
                };

                return Ok(userViewModels);
            }

            return NoContent();
        }
        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateUser(UserViewModels models)
        {
            var user = await _userManager.FindByIdAsync(models.Id);
            if (user == null) return NotFound();
            user.UserName = models.Name;
            user.City = models.City;
            user.Email = models.Email;
            user.Gender = models.Gender;
            var data = await _userManager.UpdateAsync(user);
            return data.Succeeded ? Ok(user) : Ok(models);

        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            var delete = await _userManager.DeleteAsync(user);
            return delete.Succeeded ? Ok(delete.Succeeded) : Ok(delete.Errors);
            
        }
    }
}