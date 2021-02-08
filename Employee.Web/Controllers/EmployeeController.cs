using EmployeeData.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Employee.Web.Controllers
{
    // member either admin or user can access this controller 
    [Authorize(Roles = "Admin,User")]
    // member should as admin and user can access this controller
    // start
        // [Authorize(Roles = "Admin")]
        // [Authorize(Roles = "User")]
    // end
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _employee;
        public EmployeeController(IEmployee employee)
        { 
            _employee = employee;
        }
        [HttpGet]
        [Route("employees")]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return Ok(_employee.Employees());
        }

        [HttpPost]
        public IActionResult Add(EmployeeData.Models.Employee employee)
        {
            return Ok(_employee.Add(employee));
        }
        [HttpPut]
        [Authorize(Roles = "Admin,User")]
        public IActionResult Update(EmployeeData.Models.Employee employee)
        {
            return Ok(_employee.Update(employee));
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            return Ok(_employee.Delete(id));
        }
    }
}