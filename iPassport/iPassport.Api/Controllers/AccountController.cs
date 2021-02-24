using System.Threading.Tasks;
using iPassport.Api.Models.Requests;
using iPassport.Api.Models.Responses;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace iPassport.Api.Controllers
{
    /// <summary>
    ///  AccountController
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService accountService) => _service = accountService;

        /// <summary>
        /// This API is BasicLogin
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Server returns Ok/response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpPost("LoginByAgent")]
        public async Task<ActionResult> BasicLogin([FromBody]BasicLoginRequest request)
        {
            var res = await _service.BasicLogin(request.Username, request.Password);
            return Ok(res);
        }

        /// <summary>
        /// This API is LoginByEmail
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Server returns Ok/response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpPost("LoginByEmail")]
        public async Task<ActionResult> LoginByEmail([FromBody]EmailLoginRequest request)
        {
            var res = await _service.EmailLogin(request.Email, request.Password);
            return Ok(res);
        }

        /// <summary>
        /// This API is LoginMobile
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Server returns Ok/response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpPost("LoginByCitizen")]
        public async Task<ActionResult> MobileLogin([FromBody]LoginMobileRequest request)
        {
            var res = await _service.MobileLogin(request.Pin, request.DocumentType, request.Document);
            return Ok(res);
        }

        /// <summary>
        /// This API is request PIN verification
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Server returns Ok/response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="401">Token invalid or expired</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpPost("Pin")]
        public ActionResult SendPin([FromBody] PinRequest request)
        {
            var res = _service.SendPin(request.Phone, request.doctype, request.document);
            return Ok(res);
        }
    }
}