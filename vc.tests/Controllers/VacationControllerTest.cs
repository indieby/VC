using System;
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
    public class VacationControllerTest : BaseControllerTest
    {
        [TestMethod]
        public void Get()
        {
            // Arrange
            VacationsController controller = new VacationsController(Container.Resolve<IVacationService>());

            // Act
            var result = controller.Get().ToList();

            // Assert
            Assert.AreEqual(VCSeedTestData.VacationsCount, result.Count);
        }

        [TestMethod]
        public void Post()
        {
            // Arrange
            VacationsController controller = new VacationsController(Container.Resolve<IVacationService>());

            // Act
            var result1 = controller.Post(new Vacation
            {
                From = new DateTime(2017, 11, 03),
                To = new DateTime(2017, 11, 04),
                EmployeeId = 1
            });

            var result2 = controller.Post(new Vacation
            {
                From = new DateTime(2017, 11, 03),
                To = new DateTime(2017, 11, 05),
                EmployeeId = 2
            });

            // Assert
            Assert.IsNotInstanceOfType(result1, typeof(OkResult));
            Assert.IsInstanceOfType(result2, typeof(OkResult));
        }

        [TestMethod]
        public void Put()
        {
            // Arrange
            VacationsController controller = new VacationsController(Container.Resolve<IVacationService>());

            // Act


            // Assert

        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            VacationsController controller = new VacationsController(Container.Resolve<IVacationService>());

            // Act


            // Assert

        }
    }
}