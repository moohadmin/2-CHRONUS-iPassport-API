using iPassport.Domain.Enums;
using System;

namespace iPassport.Api.Models.Requests.Company
{

    /// <summary>
    /// Get Company Request
    /// </summary>
    public class GetHeadquarterCompanyRequest
    {
        /// <summary>
        /// Cnpj Initals
        /// </summary>
        public string Cnpj { get; set; }
        /// <summary>
        /// Segment Id
        /// </summary>
        public Guid? SegmentId { get; set; }
        /// <summary>
        /// Company Type Id
        /// </summary>
        public Guid? CompanyTypeId { get; set; }
        /// <summary>
        /// Locality Id
        /// </summary>
        public Guid? LocalityId { get; set; }
    }
}
