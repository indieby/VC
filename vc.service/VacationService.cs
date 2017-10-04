using System.Collections.Generic;
using vc.data.Repositories;
using vc.model;

namespace vc.service
{
    public interface IVacationService
    {
        IEnumerable<Vacation> GetVacations();
    }

    public class VacationService : IVacationService
    {
        private readonly IVacationRepository _vacationRepository;

        public VacationService(IVacationRepository vacationRepository)
        {
            _vacationRepository = vacationRepository;
        }

        public IEnumerable<Vacation> GetVacations()
        {
            return _vacationRepository.GetAll();
        }
    }
}