using System;

namespace iPassport.Api.Models.Requests
{
    public class GetPagedVaccinesByManufacuterRequest : PageFilterRequest
    {
        public string Initials { get; set; }
        public Guid ManufacuterId { get; set; }
    }
}
