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
        /// Name Initals / Name Parts
        /// </summary>
        public string Cnpj { get; set; }

        /// <summary>
        /// Segment Identifyer
        /// </summary>
        public ECompanySegmentType SegmentIdentifyer { get; set; }

        /// <summary>
        /// State Id
        /// </summary>
        public Guid? StateId { get; set; }
        
        /// <summary>
        /// City Id
        /// </summary>
        public Guid? CityId { get; set; }
    }
}
