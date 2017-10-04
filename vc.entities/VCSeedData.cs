using System.Collections.Generic;
using System.Data.Entity;
using vc.model;

namespace vc.data
{
    public class VCSeedData : DropCreateDatabaseIfModelChanges<VCEntities>
    {
        protected override void Seed(VCEntities context)
        {
            GetPositions().ForEach(p => context.Positions.Add(p));

            context.Commit();
        }

        private static List<Position> GetPositions()
        {
            return new List<Position>
            {
                new Position
                {
                    Name = "Engineer"
                },
                new Position
                {                    
                    Name = "Economist"
                }
            };
        }
    }
}