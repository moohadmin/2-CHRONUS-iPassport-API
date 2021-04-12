using System;

namespace iPassport.Api.Models.Requests.Company
{
    /// <summary>
    /// Get Companies Paged Request Class
    /// </summary>
    public class GetCompaniesPagedRequest : PageFilterRequest
    {
        /// <summary>
        /// Name Initals / Name Parts
        /// </summary>
        public string Initials { get; set; }
        /// <summary>
        /// Cnpj
        /// </summary>
        public string Cnpj { get; set; }
        /// <summary>
        /// Type Id
        /// </summary>
        public Guid? TypeId { get; set; }
        /// <summary>
        /// Segment Id
        /// </summary>
        public Guid? SegmentId { get; set; }
        /// <summary>
        /// City Id
        /// </summary>
        public Guid? CityId { get; set; }
    }
}
