using System;
using System.Collections.Generic;
using System.Linq;
using vc.data.Infrastructure;
using vc.data.Repositories;
using vc.model;
using vc.service.HelperClasses;

namespace vc.service
{
    public interface IVacationService
    {
        IQueryable<Vacation> GetVacations();
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

        public IQueryable<Vacation> GetVacations()
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

            if (vacation != null)
            {
                if (vacation.From == default(DateTime) || vacation.To == default(DateTime) ||
                    vacation.From > vacation.To)
                    throw new VacationException();

                var vacationDays = (vacation.To - vacation.From).TotalDays;

                //Максимальное количество дней отпуска в году
                var curYear = now.Year;

                var employeeVacationsThisYear = _vacationRepository
                    .GetMany(v => v.EmployeeId == vacation.EmployeeId && v.From.Year == curYear).ToList();

                var employeeVacationsDaysThisYear = employeeVacationsThisYear
                    .Select(v => v.To.Year == curYear ? v.To - v.From : new DateTime(curYear, 12, 31) - v.From)
                    .Sum(span => span.TotalDays);

                if (employeeVacationsDaysThisYear + vacationDays > VacationRules.MaxDaysPerYear)
                    throw new VacationException(
                        $"Vacation cannot be greater than {VacationRules.MaxDaysPerYear} days per year");

                //Минимальный непрерывный период отпуска
                if (vacationDays < VacationRules.MinDays)
                    throw new VacationException($"Vacation lenght can't be less than {VacationRules.MinDays} days");

                //Максимальный непрерывный период отпуска
                if (vacationDays > VacationRules.MaxDays)
                    throw new VacationException($"Vacation lenght can't be greater than {VacationRules.MaxDays} days");

                //Минимальный период между периодами отпуска равен размеру первого отпуска (если сотрудник был в отпуске 10 дней, в последующие 10 дней он не может брать отпуск)
                var lastEmployeeVacation = employeeVacationsThisYear.OrderByDescending(v => v.To).FirstOrDefault();
                if (lastEmployeeVacation != null && vacation.From - lastEmployeeVacation.To <
                    lastEmployeeVacation.To - lastEmployeeVacation.From)
                    throw new VacationException("Period between vacations can't be less than the previous vacation");

                //В отпуске имеют право находиться не более x% сотрудников одной должности
                var employee = _employeeRepository.GetById(vacation.EmployeeId);

                if(employee == null)
                    throw new Exception("Employee not found");

                var employeePositionVacations =
                    _vacationRepository.GetMany(v =>
                        v.Employee.PositionId == employee.PositionId && v.To > now && v.From < now).Count();

                var employeePositions =
                    _employeeRepository.GetMany(e => e.PositionId == employee.PositionId).Count();

                if (employeePositionVacations + 1 > employeePositions * VacationRules.PositionOnVacationMaxProcent)
                    throw new VacationException(
                        $"On vacation, not more than {VacationRules.PositionOnVacationMaxProcent * 100}% of employees in one position");
            }
        }
    }
}