using vc.data.Infrastructure;
using vc.model;

namespace vc.data.Repositories
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }

    public interface IEmployeeRepository : IRepository<Employee>
    {
        
    }
}