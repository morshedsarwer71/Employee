using System.Threading.Tasks;
using EmployeeData.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Employee.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
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
           
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email,
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
    }
}