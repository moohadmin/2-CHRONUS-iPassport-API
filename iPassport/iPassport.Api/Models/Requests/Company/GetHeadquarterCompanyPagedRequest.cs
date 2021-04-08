using System;

namespace iPassport.Api.Models.Requests.Company
{

    /// <summary>
    /// Get Company Paged Request
    /// </summary>
    public class GetHeadquarterCompanyPagedRequest : PageFilterRequest
    {
        /// <summary>
        /// Name Initals / Name Parts
        /// </summary>
        public string Initials { get; set; }

        /// <summary>
        /// Segment Id
        /// </summary>
        public Guid? SegmentId { get; set; }

        /// <summary>
        /// State Id
        /// </summary>
        public Guid? StateId { get; set; }
    }
}
