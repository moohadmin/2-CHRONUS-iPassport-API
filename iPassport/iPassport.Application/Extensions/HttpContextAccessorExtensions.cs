using iPassport.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;

namespace iPassport.Application.Extensions
{
    public static class HttpContextAccessorExtensions
    {
        public static Guid GetCurrentUserId(this IHttpContextAccessor accessor)
        {
            var userId = accessor.HttpContext.User.FindFirst("UserId");

            if (userId == null)
                throw new BusinessException("Usuário não encontrado");

            if (!Guid.TryParse(userId.Value, out Guid result))
                throw new BusinessException("Identificador inválido");

            return result;
        }

        public static string GetCurrentToken(this IHttpContextAccessor accessor)
        {
            var authorizationHeader = accessor
            .HttpContext.Request.Headers["authorization"];

            return authorizationHeader == StringValues.Empty
                ? string.Empty
                : authorizationHeader.Single().Split(" ").Last();
        }
    }
}
