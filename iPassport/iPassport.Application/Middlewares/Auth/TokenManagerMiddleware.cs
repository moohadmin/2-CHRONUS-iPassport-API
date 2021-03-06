using iPassport.Application.Interfaces.Authentication;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;

namespace iPassport.Application.Middlewares.Auth
{
    public class TokenManagerMiddleware : IMiddleware
    {
        private readonly ITokenService _tokenService;
        
        public TokenManagerMiddleware(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (await _tokenService.IsCurrentActiveToken())
            {
                await next(context);

                return;
            }
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }
    }
}
