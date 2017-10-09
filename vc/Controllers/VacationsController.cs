using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.OData;
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

        /// <summary>
        /// List of all vacations
        /// </summary>
        /// <returns></returns>
        [EnableQuery]
        public IQueryable<Vacation> Get()
        {
            var vacations = _vacationService.GetVacations();
            return vacations;
        }

        /// <summary>
        /// Single vacation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Vacation Get(int id)
        {
            return _vacationService.GetVacations().FirstOrDefault(v => v.Id == id);
        }
        
        /// <summary>
        /// Create single vacation
        /// </summary>
        /// <param name="vacation"></param>
        /// <returns></returns>
        public IHttpActionResult Post([FromBody]Vacation vacation)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _vacationService.CreateVacation(vacation);
                _vacationService.Save();

                return StatusCode(HttpStatusCode.Created);
            }
            catch (VacationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update single vacation
        /// </summary>
        /// <param name="id"></param>
        /// <param name="vacation"></param>
        /// <returns></returns>
        public IHttpActionResult Put(int id, [FromBody] Vacation vacation)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _vacationService.UpdateVacation(id, vacation);
                _vacationService.Save();

                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (VacationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete single vacation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {
            try
            {
                _vacationService.DeleteVacation(id);
                _vacationService.Save();

                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (VacationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
