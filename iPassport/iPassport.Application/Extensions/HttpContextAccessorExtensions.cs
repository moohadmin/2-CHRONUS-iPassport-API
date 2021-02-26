using iPassport.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using System;

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
    }
}
