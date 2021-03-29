using System;

namespace iPassport.Api.Models.Requests.User
{
    /// <summary>
    /// Get Paged User Disease Test By Passport Request
    /// </summary>
    public class GetPagedUserDiseaseTestByPassportRequest : PageFilterRequest
    {
        /// <summary>
        /// Passport Id
        /// </summary>
        public Guid PassportId { get; set; }
    }
}
