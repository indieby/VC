using System.Linq;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.Results;
using vc.model;
using vc.service;
using vc.service.HelperClasses;

namespace vc.Controllers
{
    public class VacationsController : ApiController
    {
        private readonly IVacationService _vacationService;

        public VacationsController(IVacationService vacationService)
        {
            _vacationService = vacationService;
        }

        [EnableQuery]
        public IQueryable<Vacation> Get()
        {
            var vacations = _vacationService.GetVacations();
            return vacations;
        }
        
        public IHttpActionResult Post([FromBody]Vacation vacation)
        {
            try
            {
                _vacationService.CreateVacation(vacation);
                return Ok();
            }
            catch (VacationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Put(int id, [FromBody] Vacation vacation)
        {
            try
            {
                _vacationService.UpdateVacation(id, vacation);
                return Ok();
            }
            catch (VacationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Delete(int id)
        {
            try
            {
                _vacationService.DeleteVacation(id);
                return Ok();
            }
            catch (VacationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        protected override OkResult Ok()
        {
            _vacationService.Save();
            return base.Ok();
        }
    }
}
