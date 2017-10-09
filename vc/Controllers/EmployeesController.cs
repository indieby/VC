using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.OData;
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

        /// <summary>
        /// List of all vacations
        /// </summary>
        /// <returns></returns>
        [EnableQuery]
        public IQueryable<Employee> Get()
        {
            var employees = _employeeService.GetEmployees();
            return employees;
        }

        /// <summary>
        /// Single employee by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Employee Get(int id)
        {
            return _employeeService.GetEmployees().FirstOrDefault(e => e.Id == id);
        }

        /// <summary>
        /// Create single employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public IHttpActionResult Post([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _employeeService.CreateEmployee(employee);
            _employeeService.Save();

            return StatusCode(HttpStatusCode.Created);
        }
    }
}
