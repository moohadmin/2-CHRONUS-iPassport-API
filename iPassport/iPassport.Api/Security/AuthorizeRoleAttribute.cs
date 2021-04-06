using Microsoft.AspNetCore.Authorization;

namespace iPassport.Api.Security
{
    /// <summary>
    /// Authorize Role Attribute
    /// </summary>
    public class AuthorizeRoleAttribute : AuthorizeAttribute
    {
        private readonly char SEPARATOR = ',';

        /// <summary>
        /// Authorize Role Attribute empty constructor
        /// </summary>
        public AuthorizeRoleAttribute()
        {
            Roles = null;
        }

        /// <summary>
        /// AuthorizeRoleAttribute roles params constructor
        /// </summary>
        public AuthorizeRoleAttribute(params string[] roles) : this()
        {
            Roles = string.Join(SEPARATOR, roles);
        }
    }
}
