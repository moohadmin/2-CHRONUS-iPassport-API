using System;

namespace iPassport.Api.Models.Requests
{
    /// <summary>
    /// Get Paged States By Country Request model
    /// </summary>
    public class GetPagedStatesByCountryRequest : PageFilterRequest
    {
        /// <summary>
        /// Country Id
        /// </summary>
        public Guid CountryId { get; set; }
    }
}
