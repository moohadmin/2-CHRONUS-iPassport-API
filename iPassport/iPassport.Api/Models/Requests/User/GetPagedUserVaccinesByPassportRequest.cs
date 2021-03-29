using System;

namespace iPassport.Api.Models.Requests.User
{
    /// <summary>
    /// Get Paged User Vaccines By Passport Request
    /// </summary>
    public class GetPagedUserVaccinesByPassportRequest : PageFilterRequest
    {
        /// <summary>
        /// Passport Id
        /// </summary>
        public Guid PassportId { get; set; }
    }
}
