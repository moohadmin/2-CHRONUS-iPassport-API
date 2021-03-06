using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using System;
using System.Collections.Generic;

namespace iPassport.Test.Seeds
{
    public static class CompanySeed
    {
        public static Company Get() => new Company("Company1", "00560551000100", Guid.NewGuid());


        public static IList<Company> GetCompanies()
        {
            return new List<Company>()
            {
                new Company("Company1", "00560551000100", Guid.NewGuid()),
                new Company("Company2", "81851354000141", Guid.NewGuid()),
                new Company("Company3", "48387095000174", Guid.NewGuid())
            };
        }

        public static PagedData<Company> GetPaged()
        {
            return new PagedData<Company>() { Data = GetCompanies() };
        }
    }
}
