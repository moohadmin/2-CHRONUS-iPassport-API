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
        /// Segment Identifyer
        /// </summary>
        public ECompanySegmentType? SegmentIdentifyer { get; set; }
        /// <summary>
        /// Company Type Identifyer
        /// </summary>
        public ECompanyType? CompanyTypeIdentifyer { get; set; }
        /// <summary>
        /// Locality Id
        /// </summary>
        public Guid? LocalityId { get; set; }
    }
}
