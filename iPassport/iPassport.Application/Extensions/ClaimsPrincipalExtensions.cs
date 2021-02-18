using System;
using System.Security.Claims;

namespace iPassport.Application.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetLoggedUserId<Guid>(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            var loggedUserId = principal.FindFirstValue(ClaimTypes.NameIdentifier);

            return (Guid)Convert.ChangeType(loggedUserId, typeof(Guid));
        }

        public static string GetLoggedUserName(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirstValue(ClaimTypes.Name);
        }

        public static string GetLoggedUserEmail(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirstValue(ClaimTypes.Email);
        }
    }
}
