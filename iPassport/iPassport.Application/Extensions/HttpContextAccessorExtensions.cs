using iPassport.Application.Exceptions;
using iPassport.Domain.Dtos;
using iPassport.Domain.Utils;
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
                CityId = GetCurrentUserGuidClaim(context, Constants.TOKEN_CLAIM_CITY_ID),
                StateId = GetCurrentUserGuidClaim(context, Constants.TOKEN_CLAIM_STATE_ID),
                CountryId = GetCurrentUserGuidClaim(context, Constants.TOKEN_CLAIM_COUNTRY_ID),
                HealthUnityId = GetCurrentUserGuidClaim(context, Constants.TOKEN_CLAIM_HEALTH_UNITY_ID),
                CompanyId = GetCurrentUserGuidClaim(context, Constants.TOKEN_CLAIM_COMPANY_ID)
            };

        private static Guid GetUserId(ClaimsPrincipal user)
        {
            var userId = user.FindFirst(Constants.TOKEN_CLAIM_USER_ID);

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
