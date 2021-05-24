using System;

namespace iPassport.Domain.Filters
{
    public class GetVaccineByManufacturerFilter : GetPagedVaccinesFilter
    {
        public DateTime Birthday { get; set; }
    }
}
