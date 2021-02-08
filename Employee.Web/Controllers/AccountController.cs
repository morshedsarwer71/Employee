using System.Threading.Tasks;
using EmployeeData.Context;
using EmployeeData.ViewModels;
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
    }
}