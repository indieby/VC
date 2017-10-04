using vc.data.Infrastructure;
using vc.model;

namespace vc.data.Repositories
{
    public class PositionRepository: RepositoryBase<Position>, IPositionRepository
    {
        public PositionRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }

    public interface IPositionRepository : IRepository<Position>
    {
        
    }
}