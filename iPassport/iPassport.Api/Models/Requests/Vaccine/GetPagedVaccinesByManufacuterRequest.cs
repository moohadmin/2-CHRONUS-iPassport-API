using System;

namespace iPassport.Api.Models.Requests.Vaccine
{
    /// <summary>
    /// Get Paged Vaccines By Manufacuter Request model
    /// </summary>
    public class GetPagedVaccinesByManufacuterRequest : PageFilterRequest
    {
        /// <summary>
        /// Initials
        /// </summary>
        public string Initials { get; set; }
        /// <summary>
        /// Manufacuter Id
        /// </summary>
        public Guid ManufacuterId { get; set; }
        /// <summary>
        /// Birthday
        /// </summary>
        public DateTime Birthday { get; set; }
    }
}
