using iPassport.Api.Models.Responses;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace iPassport.Api.Controllers
{
    /// <summary>
    ///  AuthController
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuth2FactService _auth2FactService;
        public AuthController(IAuth2FactService auth2FactService)
        {
            _auth2FactService = auth2FactService;
        }

        /// <summary>
        /// Teste Envio de SMS
        /// </summary>
        /// <returns></returns>
        /// <response code="204">Server returns no data.</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpPost]
        public OkResult Post()
        {
            _auth2FactService.AuthClientSend();
            return Ok();
        }

        

        /// <summary>
        /// Teste Consulta de Envio de SMS
        /// </summary>
        /// <returns></returns>
        /// <response code="204">Server returns no data.</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpGet]
        public OkResult Get()
        {
            _auth2FactService.AuthClientRecieve();
            return Ok();
        }


        
    }
}
