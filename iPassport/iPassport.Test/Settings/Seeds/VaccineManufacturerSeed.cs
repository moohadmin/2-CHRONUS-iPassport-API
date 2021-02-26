using iPassport.Application.Models.Pagination;
using iPassport.Domain.Entities;
using System.Collections.Generic;

namespace iPassport.Test.Seeds
{
    public static class VaccineManufacturerSeed
    {
        public static PagedResponseApi GetPagedVaccineManufacturer()
        {
            return new PagedResponseApi(true, null, 1, 3, 10, 300, new List<VaccineManufacturer>() { new VaccineManufacturer("Test"), new VaccineManufacturer("Test-2"), new VaccineManufacturer("Test-33") });
        }
    }
}
