using System.Data.Entity.ModelConfiguration;
using vc.model;

namespace vc.data.Configuration
{
    public class PositionConfiguration : EntityTypeConfiguration<Position>
    {
        public PositionConfiguration()
        {
            ToTable("Position");
            Property(p => p.Name).IsRequired();
        }
    }
}