using System.Data.Entity.ModelConfiguration;
using vc.model;

namespace vc.data.Configuration
{
    public class VacationConfiguration : EntityTypeConfiguration<Vacation>
    {
        public VacationConfiguration()
        {
            ToTable("Vacation");
            Property(v => v.EmployeeId).IsRequired();
            Property(v => v.From).IsRequired();
            Property(v => v.To).IsRequired();
        }
    }
}