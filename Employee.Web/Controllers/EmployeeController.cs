using EmployeeData.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Employee.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployee _employee;

        public EmployeeController(IEmployee employee)
        {
            _employee = employee;
        }
        [HttpGet]
        [Route("employees")]
        public IActionResult Index()
        {
            return Ok(_employee.Employees());
        }
    }
}