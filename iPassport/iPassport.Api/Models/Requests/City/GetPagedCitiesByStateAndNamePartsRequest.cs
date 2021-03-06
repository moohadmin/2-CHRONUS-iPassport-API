using System;

namespace iPassport.Api.Models.Requests
{
    public class GetPagedCitiesByStateAndNamePartsRequest : PageFilterRequest
    {
        public Guid StateId { get; set; }
        public string Initials { get; set; }
    }
}
