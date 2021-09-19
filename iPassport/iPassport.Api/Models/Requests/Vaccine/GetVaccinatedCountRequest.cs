using System;

namespace iPassport.Api.Models.Requests
{
    /// <summary>
    /// Get Vaccinated Count Request model
    /// </summary>
    public class GetVaccinatedCountRequest
    {
        /// <summary>
        /// Start Time
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// End Time
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// Disease Id
        /// </summary>
        public Guid? DiseaseId { get; set; }
        /// <summary>
        /// Manufacturer Id
        /// </summary>
        public Guid? ManufacturerId { get; set; }
        /// <summary>
        /// Dosage Count
        /// </summary>
        public int DosageCount { get; set; }
    }
}
