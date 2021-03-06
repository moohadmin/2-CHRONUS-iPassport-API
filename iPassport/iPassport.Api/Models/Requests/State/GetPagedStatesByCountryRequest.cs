using System;

namespace iPassport.Api.Models.Requests
{
    public class GetPagedStatesByCountryRequest : PageFilterRequest
    {
        public Guid CountryId { get; set; }
    }
}
