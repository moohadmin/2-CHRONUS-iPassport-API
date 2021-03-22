using iPassport.Api.Models.Requests.Shared;

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
    }
}
