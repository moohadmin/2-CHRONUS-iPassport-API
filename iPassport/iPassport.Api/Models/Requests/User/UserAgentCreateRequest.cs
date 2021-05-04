using System;

namespace iPassport.Api.Models.Requests.User
{
    /// <summary>
    /// Create User Agent Request
    /// </summary>
    public class UserAgentCreateRequest
    {
        /// <summary>
        /// Cpf
        /// </summary>
        public string Cpf { get; set; }

        /// <summary>
        /// E-mail
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Cellphone Number
        /// </summary>
        public string CellphoneNumber { get; set; }

        /// <summary>
        /// Corporate Cellphone Number
        /// </summary>
        public string CorporateCellphoneNumber { get; set; }

        /// <summary>
        /// Complete Name
        /// </summary>
        public string CompleteName { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        public AddressCreateRequest Address { get; set; }

        /// <summary>
        /// Company Id
        /// </summary>
        public Guid? CompanyId { get; set; }

        /// <summary>
        /// Is User Active
        /// </summary>
        public bool? IsActive { get; set; }
    }
}
