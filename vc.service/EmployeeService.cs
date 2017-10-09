using System.Collections.Generic;
using System.Linq;
using vc.data.Infrastructure;
using vc.data.Repositories;
using vc.model;

namespace vc.service
{
    public interface IEmployeeService
    {
        IQueryable<Employee> GetEmployees();
        void CreateEmployee(Employee employee);
        void Save();
    }

    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }

        public IQueryable<Employee> GetEmployees()
        {
            return _employeeRepository.GetAll();
        }

        public void CreateEmployee(Employee employee)
        {
            _employeeRepository.Add(employee);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}