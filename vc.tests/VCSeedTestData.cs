using System;
using System.Collections.Generic;
using System.Data.Entity;
using vc.data;
using vc.model;

namespace vc.tests
{
    public class VCSeedTestData : DropCreateDatabaseAlways<VCEntities>
    {
        public static int EmployeesCount => GetEmployees().Count;
        public static int VacationsCount => TestVacations.Count;

        protected override void Seed(VCEntities context)
        {
            GetPositions().ForEach(p => context.Positions.Add(p));

            context.Commit();

            GetEmployees().ForEach(e => context.Employees.Add(e));

            context.Commit();

            TestVacations.ForEach(v => context.Vacations.Add(v));

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
                    PositionId = 2
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
                new Employee
                {
                    FirstName = "Annette",
                    LastName = "Dennis",
                    PositionId = 1
                },
                new Employee
                {
                    FirstName = "Terri",
                    LastName = "Abbott",
                    PositionId = 1
                },
            };
        }

        public static List<Vacation> TestVacations = new List<Vacation>();
    }
}