using System;

namespace iPassport.Api.Models.Requests
{
    public class GetPagedVaccinesByManufacuterRequest : PageFilterRequest
    {
        public Guid ManufacuterId { get; set; }
    }
}
