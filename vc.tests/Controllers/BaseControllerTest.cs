using System.Data.Entity;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vc.data;
using vc.data.Infrastructure;
using vc.data.Repositories;
using vc.service;

namespace vc.tests.Controllers
{
    public class BaseControllerTest
    {
        protected IContainer Container;

        [TestInitialize]
        public void Setup()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<TestDbFactory>().As<IDbFactory>().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(EmployeeRepository).Assembly).InstancePerLifetimeScope()
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(EmployeeService).Assembly).InstancePerLifetimeScope()
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces();

            Container = builder.Build();

            Database.SetInitializer(new VCSeedTestData());
        }
    }
}