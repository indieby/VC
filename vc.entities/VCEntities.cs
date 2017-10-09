using System.Data.Common;
using System.Data.Entity;
using vc.data.Configuration;
using vc.model;

namespace vc.data
{
    public class VCEntities : DbContext 
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Vacation> Vacations { get; set; }

        public virtual void Commit()
        {
            SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EmployeeConfiguration());
            modelBuilder.Configurations.Add(new PositionConfiguration());
            modelBuilder.Configurations.Add(new VacationConfiguration());
        }

        public VCEntities()
        {
        }

        public VCEntities(DbConnection connection) : base(connection, false)
        {            
        }
    }
}