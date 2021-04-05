using System;

namespace iPassport.Api.Models.Requests.User
{

    /// <summary>
    /// Get Admin User Paged Request
    /// </summary>
    public class GetAdminUserPagedRequest : PageFilterRequest
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Cpf
        /// </summary>
        public string Cpf { get; set; }
        /// <summary>
        /// Company Id
        /// </summary>
        public Guid? CompanyId { get; set; }
        /// <summary>
        /// Profile Id
        /// </summary>
        public Guid? ProfileId { get; set; }
    }
}
