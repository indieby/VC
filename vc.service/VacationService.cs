using System;
using System.Collections.Generic;
using System.Linq;
using vc.data.Infrastructure;
using vc.data.Repositories;
using vc.model;

namespace vc.service
{
    public interface IVacationService
    {
        IEnumerable<Vacation> GetVacations();
        void CreateVacation(Vacation vacation);
        void UpdateVacation(int id, Vacation vacation);
        void DeleteVacation(int id);
        void Save();
    }

    public class VacationService : IVacationService
    {
        private readonly IVacationRepository _vacationRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public VacationService(IVacationRepository vacationRepository, IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
        {
            _vacationRepository = vacationRepository;
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Vacation> GetVacations()
        {
            return _vacationRepository.GetAll();
        }

        public void CreateVacation(Vacation vacation)
        {
            Validate(null, vacation);
            _vacationRepository.Add(vacation);
        }

        public void UpdateVacation(int id, Vacation vacation)
        {
            vacation.Id = id;
            Validate(id, vacation);
            _vacationRepository.Update(vacation);
        }

        public void DeleteVacation(int id)
        {
            Validate(id, null);
            _vacationRepository.Delete(vacation => vacation.Id == id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        void Validate(int? id, Vacation vacation)
        {
            var now = DateTime.UtcNow;

            if (id.HasValue)
            {
                var curVacation = _vacationRepository.GetById(id.Value);

                if (curVacation.From < now)
                    throw new VacationException("Editing or deleting can be done only for the upcoming vacation");
            }

            var vacationDays = (vacation.To - vacation.From).TotalDays;

            //Максимальное количество дней отпуска в году - 24 календарных дня
            var curYear = now.Year;

            var employeeVacationsThisYear = _vacationRepository
                .GetMany(v => v.EmployeeId == vacation.EmployeeId && v.From.Year == curYear).ToList();

            var employeeVacationsDaysThisYear = employeeVacationsThisYear
                .Select(v => v.To.Year == curYear ? v.To - v.From : new DateTime(curYear, 12, 31) - v.From)
                .Sum(span => span.TotalDays);

            if (employeeVacationsDaysThisYear + vacationDays > 24)
                throw new VacationException("Vacation cannot be greater than 24 days per year");

            //Минимальный непрерывный период отпуска - 2 календарных дня
            if (vacationDays < 2)
                throw new VacationException("Vacation lenght can't be less than 2 days");

            //Максимальный непрерывный период отпуска - 15 календарных дней
            if (vacationDays > 15)
                throw new VacationException("Vacation lenght can't be greater than 15 days");

            //Минимальный период между периодами отпуска равен размеру первого отпуска (если сотрудник был в отпуске 10 дней, в последующие 10 дней он не может брать отпуск)
            var lastEmployeeVacation = employeeVacationsThisYear.OrderByDescending(v => v.To).FirstOrDefault();
            if (lastEmployeeVacation != null && vacation.From - lastEmployeeVacation.To < lastEmployeeVacation.To - lastEmployeeVacation.From)
                throw new VacationException("Period between vacations can't be less than the previous vacation");

            //В отпуске имеют право находиться не более 50% сотрудников одной должности (если в компании 5 бухгалтеров, одновременно в отпуске может быть не более 2)
            var employee = _employeeRepository.GetById(vacation.EmployeeId);
            
            var employeePositionVacations =
                _vacationRepository.GetMany(v =>
                    v.Employee.PositionId == employee.PositionId && v.To > now).Count();

            var employeePositions =
                _employeeRepository.GetMany(e => e.PositionId == employee.PositionId).Count();

            if (employeePositionVacations + 1 > employeePositions / 2)
                throw new VacationException("On vacation, not more than 50% of employees in one position");
        }
    }
}