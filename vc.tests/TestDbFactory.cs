using System.Data.Common;
using System.IO;
using vc.data;
using vc.data.Infrastructure;

namespace vc.tests
{
    public class TestDbFactory : IDbFactory
    {
        private VCEntities _dbContext;

        public VCEntities Init()
        {
            string connectionString = $"Data Source={Path.GetTempFileName()};Persist Security Info=False";

            var conn = DbProviderFactories.GetFactory("System.Data.SqlServerCe.4.0").CreateConnection();
            conn.ConnectionString = connectionString;
            return _dbContext ?? (_dbContext = new VCEntities(conn));
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}