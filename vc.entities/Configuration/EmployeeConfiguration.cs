using System.Data.Entity.ModelConfiguration;
using vc.model;

namespace vc.data.Configuration
{
    public class EmployeeConfiguration : EntityTypeConfiguration<Employee>
    {
        public EmployeeConfiguration()
        {
            ToTable("Employee");
            Property(e => e.PositionId).IsRequired();
        }
    }
}