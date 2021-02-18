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
        /// <response code="204">Server returns no data.</response>
        /// <response code="400">Bussiness Exception</response>
        /// <response code="500">Due to server problems, it is not possible to get your data now</response>
        [ProducesResponseType(typeof(ResponseApi), 200)]
        [ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        [ProducesResponseType(typeof(ServerErrorResponse), 500)]
        [HttpPost("BasicLogin")]
        public async Task<ActionResult> BasicLogin([FromBody]BasicLoginRequest request)
        {
            var res = await _service.BasicLogin(request.Username, request.Password);
            return Ok(res);
        }

        ///// <summary>
        ///// This API is LoginWithEmail
        ///// </summary>
        ///// <returns></returns>
        ///// <response code="204">Server returns no data.</response>
        ///// <response code="400">Bussiness Exception</response>
        ///// <response code="500">Due to server problems, it is not possible to get your data now</response>
        //[ProducesResponseType(typeof(ResponseApi), 200)]
        //[ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        //[ProducesResponseType(typeof(ServerErrorResponse), 500)]
        //[HttpPost]
        //public async Task<ActionResult> LoginWithEmail()
        //{
        //    var res = await _service.LoginWithEmail();
        //    return Ok(res);
        //}

        ///// <summary>
        ///// This API is LoginMobile
        ///// </summary>
        ///// <returns></returns>
        ///// <response code="204">Server returns no data.</response>
        ///// <response code="400">Bussiness Exception</response>
        ///// <response code="500">Due to server problems, it is not possible to get your data now</response>
        //[ProducesResponseType(typeof(ResponseApi), 200)]
        //[ProducesResponseType(typeof(BussinessExceptionResponse), 400)]
        //[ProducesResponseType(typeof(ServerErrorResponse), 500)]
        //[HttpPost]
        //public async Task<ActionResult> LoginMobile()
        //{
        //    var res = await _service.LoginMobile();
        //    return Ok(res);
        //}
    }
}