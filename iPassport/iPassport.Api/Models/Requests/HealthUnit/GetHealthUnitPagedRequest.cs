using iPassport.Api.Models.Requests.Shared;
using System;

namespace iPassport.Api.Models.Requests.HealthUnit
{
    /// <summary>
    /// Get Health Unit Paged Request
    /// </summary>
    public class GetHealthUnitPagedRequest : GetByNamePartsPagedRequest
    {
        /// <summary>
        /// Cnpj
        /// </summary>
        public string Cnpj { get; set; }
        
        /// <summary>
        /// Ine
        /// </summary>
        public string Ine { get; set; }

        /// <summary>
        /// Company Id
        /// </summary>
        public Guid? CompanyId { get; set; }

        /// <summary>
        /// TypeId
        /// </summary>
        public Guid? TypeId { get; set; }
    }
}
