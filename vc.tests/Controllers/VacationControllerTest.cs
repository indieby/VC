using System;
using System.Collections.Generic;
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
        public void PostMaxLenght()
        {
            // Arrange
            VacationsController controller = new VacationsController(Container.Resolve<IVacationService>());

            //Act
            var start = DateTime.UtcNow;
            
            //1. Try to create more than 15-days vacation 
            var result1 = controller.Post(new Vacation
            {
                From = start,
                To = start.AddDays(16),
                EmployeeId = 5 //engineer
            });

            //2. Try to create 15-days vacation 
            var result2 = controller.Post(new Vacation
            {
                From = start,
                To = start.AddDays(15),
                EmployeeId = 5 //engineer
            });


            // Assert
            Assert.IsNotInstanceOfType(result1, typeof(OkResult));
            Assert.IsInstanceOfType(result2, typeof(OkResult));
        }

        [TestMethod]
        public void PostMinLenght()
        {
            // Arrange
            VacationsController controller = new VacationsController(Container.Resolve<IVacationService>());

            // Act
            var start = DateTime.UtcNow;
            //1. Try to create 1-day vacation 
            var result1 = controller.Post(new Vacation
            {
                From = start,
                To = start.AddDays(1),
                EmployeeId = 5 //engineer
            });

            //2. Try to create 2-days vacation
            var result2 = controller.Post(new Vacation
            {
                From = start,
                To = start.AddDays(2),
                EmployeeId = 5 //engineer
            });

            // Assert
            Assert.IsNotInstanceOfType(result1, typeof(OkResult));
            Assert.IsInstanceOfType(result2, typeof(OkResult));
        }

        [TestMethod]
        public void PostPositionVacationProcent()
        {
            // Arrange
            VacationsController controller = new VacationsController(Container.Resolve<IVacationService>());

            //Act
            //1. Try to create vacation for economist (1 of 1 in company)
            var result1 = controller.Post(new Vacation
            {
                From = new DateTime(2017, 11, 03),
                To = new DateTime(2017, 11, 05),
                EmployeeId = 1 //economist
            });

            //2. Try to create vacation for engineer (1 of 4 in company)
            var result2 = controller.Post(new Vacation
            {
                From = new DateTime(2017, 11, 03),
                To = new DateTime(2017, 11, 05),
                EmployeeId = 2 //engineer
            });

            //3. Try to create vacation for engineer (2 of 4 in company)
            var result3 = controller.Post(new Vacation
            {
                From = new DateTime(2017, 11, 03),
                To = new DateTime(2017, 11, 05),
                EmployeeId = 3 //engineer
            });

            //4. Try to create vacation for engineer (3 of 4 in company)
            var result4 = controller.Post(new Vacation
            {
                From = new DateTime(2017, 11, 03),
                To = new DateTime(2017, 11, 05),
                EmployeeId = 3 //engineer
            });

            //Assert
            Assert.IsNotInstanceOfType(result1, typeof(OkResult));
            Assert.IsInstanceOfType(result2, typeof(OkResult));
            Assert.IsInstanceOfType(result3, typeof(OkResult));
            Assert.IsNotInstanceOfType(result4, typeof(OkResult));
        }

        [TestMethod]
        public void PostVacationMaxYearLength()
        {
            // Arrange
            VacationsController controller = new VacationsController(Container.Resolve<IVacationService>());
            
            //Act
            var employeeId = 5; //employee

            //1. Create vacation (15 days)
            var result1 = controller.Post(new Vacation
            {
                From = new DateTime(2017, 11, 03),
                To = new DateTime(2017, 11, 17),
                EmployeeId = employeeId
            });

            //2. Create other one (15 days)
            var result2 = controller.Post(new Vacation
            {
                From = new DateTime(2017, 12, 03),
                To = new DateTime(2017, 12, 17),
                EmployeeId = employeeId
            });

            //3. Create other one (9 days)
            var result3 = controller.Post(new Vacation
            {
                From = new DateTime(2017, 12, 03),
                To = new DateTime(2017, 12, 11),
                EmployeeId = employeeId
            });

            //Assert
            Assert.IsInstanceOfType(result1, typeof(OkResult));
            Assert.IsNotInstanceOfType(result2, typeof(OkResult));
            Assert.IsInstanceOfType(result3, typeof(OkResult));
        }

        [TestMethod]
        public void PostMinPeriodBetween()
        {
            // Arrange
            VacationsController controller = new VacationsController(Container.Resolve<IVacationService>());

            //Act
            var employeeId = 5; //employee
            var start = DateTime.UtcNow;
            var finish = start.AddDays(14);

            //1. Create vacation (14 days)
            var result1 = controller.Post(new Vacation
            {
                From = start,
                To = finish,
                EmployeeId = employeeId
            });

            //2. Create other one after 13 days
            var result2 = controller.Post(new Vacation
            {
                From = finish.AddDays(13),
                To = finish.AddDays(19),
                EmployeeId = employeeId
            });

            //3. Create other one after 14 days
            var result3 = controller.Post(new Vacation
            {
                From = finish.AddDays(14),
                To = finish.AddDays(16),
                EmployeeId = employeeId
            });

            //Assert
            Assert.IsInstanceOfType(result1, typeof(OkResult));
            Assert.IsNotInstanceOfType(result2, typeof(OkResult));
            Assert.IsInstanceOfType(result3, typeof(OkResult));
        }

        [TestMethod]
        public void Put()
        {
            // Arrange
            VacationsController controller = new VacationsController(Container.Resolve<IVacationService>());
            VCSeedTestData.TestVacations.AddRange(new List<Vacation>
            {
                new Vacation
                {
                    EmployeeId = 3,
                    From = DateTime.UtcNow.AddDays(-5),
                    To = DateTime.UtcNow.AddDays(-1)
                },
                new Vacation
                {
                    EmployeeId = 3,
                    From = DateTime.UtcNow.AddDays(1),
                    To = DateTime.UtcNow.AddDays(5)
                },
            });

            // Act
            //1. Try to update past vacation
            var result1 = controller.Put(1, new Vacation
            {
                From = new DateTime(2017, 10, 1),
                To = new DateTime(2017, 10, 11),
                EmployeeId = 3
            });

            //2. Try to update upcoming vacation
            var result2 = controller.Put(2, new Vacation
            {
                From = new DateTime(2017, 10, 8),
                To = new DateTime(2017, 10, 11),
                EmployeeId = 5
            });

            // Assert
            Assert.IsNotInstanceOfType(result1, typeof(OkResult));
            Assert.IsInstanceOfType(result2, typeof(OkResult));
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            VacationsController controller = new VacationsController(Container.Resolve<IVacationService>());
            VCSeedTestData.TestVacations.AddRange(new List<Vacation>
            {
                new Vacation
                {
                    EmployeeId = 3,
                    From = DateTime.UtcNow.AddDays(-5),
                    To = DateTime.UtcNow.AddDays(-1)
                },
                new Vacation
                {
                    EmployeeId = 3,
                    From = DateTime.UtcNow.AddDays(1),
                    To = DateTime.UtcNow.AddDays(5)
                },
            });

            // Act
            //1. Try to delete past vacation
            var result1 = controller.Delete(1);

            //2. Try to delete upcoming vacation
            var result2 = controller.Delete(2);

            // Assert
            Assert.IsNotInstanceOfType(result1, typeof(OkResult));
            Assert.IsInstanceOfType(result2, typeof(OkResult));
        }
    }
}