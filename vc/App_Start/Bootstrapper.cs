using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using vc.data;
using vc.data.Infrastructure;
using vc.data.Repositories;
using vc.service;

namespace vc
{
    public class Bootstrapper
    {
        public static IContainer Container { get; private set; }

        public static void Run()
        {
            SetAutofacContainer();
        }

        static void SetAutofacContainer()
        {
            var builder = new ContainerBuilder();
            
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(EmployeeRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(EmployeeService).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            Container = builder.Build();
        }
    }
}