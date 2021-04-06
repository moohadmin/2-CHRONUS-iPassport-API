using System;

namespace iPassport.Api.Models.Requests
{
    /// <summary>
    /// Get Paged Cities By State And Name Parts Request model
    /// </summary>
    public class GetPagedCitiesByStateAndNamePartsRequest : PageFilterRequest
    {
        /// <summary>
        /// State Id
        /// </summary>
        public Guid StateId { get; set; }
        /// <summary>
        /// Initials
        /// </summary>
        public string Initials { get; set; }
    }
}
