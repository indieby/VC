using vc.data.Infrastructure;

namespace vc.data
{
    public class DbFactory : IDbFactory
    {
        private VCEntities _dbContext;

        public VCEntities Init()
        {
            return _dbContext ?? (_dbContext = new VCEntities());
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}