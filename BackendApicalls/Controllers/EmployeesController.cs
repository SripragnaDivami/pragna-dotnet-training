
using Microsoft.AspNetCore.Mvc;


namespace BackendApicalls.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly Services.EmployeeService employeeService;
        public EmployeesController(Services.EmployeeService employeesService)
        {
            employeeService = employeesService;
        }
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var employess_data=await employeeService.GetAllEmployeesAsync();
            return Ok(employess_data);
        }
    }
}
