using System;

namespace iPassport.Api.Models.Requests.User
{
    /// <summary>
    /// Admin Create Request
    /// </summary>
    public class AdminCreateRequest
    {
        /// <summary>
        /// Complete Name
        /// </summary>
        public string CompleteName { get; set; }
        /// <summary>
        /// Cpf
        /// </summary>
        public string Cpf { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Telephone
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// Company Id
        /// </summary>
        public Guid? CompanyId { get; set; }
        /// <summary>
        /// Health Unit Id
        /// </summary>
        public Guid? HealthUnitId { get; set; }
        /// <summary>
        /// Occupation
        /// </summary>
        public string Occupation { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Acess Profile Id
        /// </summary>
        public Guid? profileId { get; set; }
        /// <summary>
        /// Is User Active
        /// </summary>
        public bool? IsActive { get; set; }
    }
}
