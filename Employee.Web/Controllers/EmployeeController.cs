using EmployeeData.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Employee.Web.Controllers
{
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
        public IActionResult Update(EmployeeData.Models.Employee employee)
        {
            return Ok(_employee.Update(employee));
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            return Ok(_employee.Delete(id));
        }
    }
}