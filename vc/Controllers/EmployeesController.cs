using System.Web.Http;
using vc.model;
using vc.service;

namespace vc.Controllers
{
    public class EmployeesController : ApiController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public IHttpActionResult Get()
        {
            var employees = _employeeService.GetEmployees();
            return Ok(employees);
        }

        public IHttpActionResult Post([FromBody] Employee employee)
        {
            _employeeService.CreateEmployee(employee);
            _employeeService.Save();

            return Ok();
        }
    }
}
