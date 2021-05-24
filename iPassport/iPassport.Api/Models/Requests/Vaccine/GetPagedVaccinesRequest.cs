using System;

namespace iPassport.Api.Models.Requests
{
    /// <summary>
    /// Get Paged Vaccines Request model
    /// </summary>
    public class GetPagedVaccinesRequest : PageFilterRequest
    {
        /// <summary>
        /// Initials
        /// </summary>
        public string Initials { get; set; }
        /// <summary>
        /// Manufacuter Id
        /// </summary>
        public Guid? ManufacuterId { get; set; }
        /// <summary>
        /// Disease Id
        /// </summary>
        public Guid? DiseaseId { get; set; }
        /// <summary>
        /// Dosage Type Id
        /// </summary>
        public Guid? DosageTypeId { get; set; }
    }
}
