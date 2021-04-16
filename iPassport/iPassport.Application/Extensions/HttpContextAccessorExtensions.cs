using iPassport.Application.Exceptions;
using iPassport.Domain.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;
using System.Security.Claims;

namespace iPassport.Application.Extensions
{
    public static class HttpContextAccessorExtensions
    {
        public static Guid GetCurrentUserId(this IHttpContextAccessor accessor) =>
            GetUserId(accessor.HttpContext.User);

        public static Guid GetCurrentUserId(this HttpContext context) =>
            GetUserId(context.User);

        public static string GetCurrentToken(this IHttpContextAccessor accessor)
        {
            var authorizationHeader = accessor
            .HttpContext.Request.Headers["authorization"];

            return authorizationHeader == StringValues.Empty
                ? string.Empty
                : authorizationHeader.Single().Split(" ").Last();
        }

        public static AccessControlDTO GetAccessControlDTO(this IHttpContextAccessor context) =>
            new AccessControlDTO
            {
                Profile = GetCurrentUserProfile(context),
                CityId = GetCurrentUserGuidClaim(context, "CityId"),
                StateId = GetCurrentUserGuidClaim(context, "StateId"),
                CountryId = GetCurrentUserGuidClaim(context, "CountryId"),
                HealthUnityId = GetCurrentUserGuidClaim(context, "HealthUnityId"),
                CompanyId = GetCurrentUserGuidClaim(context, "CompanyId")
            };

        private static Guid GetUserId(ClaimsPrincipal user)
        {
            var userId = user.FindFirst("UserId");

            if (userId == null)
                throw new BusinessException("Usuário não encontrado");

            if (!Guid.TryParse(userId.Value, out Guid result))
                throw new BusinessException("Usuário inválido");

            return result;
        }

        public static string GetCurrentUserProfile(this IHttpContextAccessor context)
        {
            var profile = context.HttpContext.User.FindFirst(ClaimTypes.Role);

            if (profile == null)
                throw new BusinessException("Perfil não encontrado");

            if (string.IsNullOrWhiteSpace(profile.Value))
                throw new BusinessException("Perfil inválido");

            return profile.Value;
        }

        public static Guid? GetCurrentUserGuidClaim(this IHttpContextAccessor context, string claimName)
        {
            var claimValue = context.HttpContext.User.FindFirst(claimName);

            if (claimValue == null)
                throw new BusinessException(string.Format("{0} não encontrada", claimName));

            if (string.IsNullOrWhiteSpace(claimValue.Value))
                return null;

            if (!Guid.TryParse(claimValue.Value, out Guid result))
                throw new BusinessException(string.Format("{0} inválida", claimName));

            return result;
        }
    }
}
