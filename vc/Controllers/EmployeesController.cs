using System.Linq;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.Results;
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

        [EnableQuery]
        public IQueryable<Employee> Get()
        {
            var employees = _employeeService.GetEmployees();
            return employees;
        }

        public IHttpActionResult Post([FromBody] Employee employee)
        {
            _employeeService.CreateEmployee(employee);
            return Ok();
        }

        protected override OkResult Ok()
        {
            _employeeService.Save();
            return base.Ok();
        }
    }
}
