using vc.data.Infrastructure;
using vc.model;

namespace vc.data.Repositories
{
    public class VacationRepository : RepositoryBase<Vacation>, IVacationRepository
    {
        public VacationRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }

    public interface IVacationRepository : IRepository<Vacation>
    {
        
    }
}