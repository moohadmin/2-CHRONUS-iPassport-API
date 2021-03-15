using System;

namespace iPassport.Api.Models.Requests.User
{
    /// <summary>
    /// Create User Agent Request
    /// </summary>
    public class UserAgentCreateRequest
    {
        /// <summary>
        /// Full Name
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Cpf
        /// </summary>
        public string CPF { get; set; }

        /// <summary>
        /// Username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Mobile
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        public AddressCreateRequest Address { get; set; }

        /// <summary>
        /// Company Id
        /// </summary>
        public Guid CompanyId { get; set; }

        /// <summary>
        /// Password Is Valid ?
        /// </summary>
        public bool PasswordIsValid { get; set; }
        
    }
}
