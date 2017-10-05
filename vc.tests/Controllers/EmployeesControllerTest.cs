using System.Linq;
using System.Web.Http.Results;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vc.Controllers;
using vc.model;
using vc.service;

namespace vc.tests.Controllers
{
    [TestClass]
    public class EmployeesControllerTest : BaseControllerTest
    {
        [TestMethod]
        public void Get()
        {
            // Arrange
            EmployeesController controller = new EmployeesController(Container.Resolve<IEmployeeService>());

            // Act
            var result = controller.Get().ToList();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(VCSeedTestData.EmployeesCount, result.Count);
        }
        
        [TestMethod]
        public void Post()
        {
            // Arrange
            EmployeesController controller = new EmployeesController(Container.Resolve<IEmployeeService>());

            // Act
            var result = controller.Post(new Employee {FirstName = "Test1", PositionId = 1});

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }
    }
}
