using System;
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

            GetEmployees().ForEach(e => context.Employees.Add(e));

            context.Commit();

            GetVacations().ForEach(v => context.Vacations.Add(v));

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

        private static List<Employee> GetEmployees()
        {
            return new List<Employee>
            {
                new Employee
                {
                    FirstName = "Rudy",
                    LastName = "Adkins",
                    PositionId = 1
                },
                new Employee
                {
                    FirstName = "Bradley",
                    LastName = "Mcdaniel",
                    PositionId = 1
                },
                new Employee
                {
                    FirstName = "Irene",
                    LastName = "Patrick",
                    PositionId = 1
                },
            };
        }

        private static List<Vacation> GetVacations()
        {
            return new List<Vacation>
            {
                new Vacation
                {
                    EmployeeId = 1,
                    From = new DateTime(2017, 03, 01),
                    To = new DateTime(2017, 03, 20)
                },
                new Vacation
                {
                    EmployeeId = 2,
                    From = new DateTime(2017, 05, 20),
                    To = new DateTime(2017, 06, 01)
                },
                new Vacation
                {
                    EmployeeId = 3,
                    From = new DateTime(2017, 11, 01),
                    To = new DateTime(2017, 11, 20)
                },
                new Vacation
                {
                    EmployeeId = 2,
                    From = new DateTime(2017, 07, 01),
                    To = new DateTime(2017, 07, 06)
                }
            };
        }
    }
}