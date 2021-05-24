using iPassport.Api.Models.Responses;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace iPassport.Api.Controllers
{
    /// <summary>
    /// Vaccine Period Type Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VaccineDosageTypeController : ControllerBase
    {
        /// <summary>
        /// Vaccine Dosage Type Service Property
        /// </summary>
        private readonly IVaccineDosageTypeService _service;

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="service">Vaccine Dosage Type Service Instance</param>
        public VaccineDosageTypeController(IVaccineDosageTypeService service)
        {
            _service = service;
        }

        /// <summary>
        /// This API is responsible for get Vaccine Dosage Type list.
        /// </summary>
        /// <response code="200">Server returns Ok</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response> 
        /// <returns>Vaccine Dosage Type list.</returns>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpGet]
        public async Task<ActionResult> GetAll() =>
            Ok(await _service.GetAll());
    }
}
