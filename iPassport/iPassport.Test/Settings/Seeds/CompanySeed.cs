using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using System;
using System.Collections.Generic;

namespace iPassport.Test.Seeds
{
    public static class CompanySeed
    {
        public static Company Get() => new Company("Company1", "TradeName", "00560551000100", GetAddress(), Guid.NewGuid(), null, Guid.NewGuid(), null);

        private static AddressCreateDto GetAddress() => new AddressCreateDto
        {
            Cep = "43700000",
            CityId = Guid.NewGuid(),
            Description = "Description"
        };
        public static IList<Company> GetCompanies()
        {
            return new List<Company>()
            {
                new Company("Company2","TradeName" ,"00560551000100", GetAddress(), Guid.NewGuid(), null,Guid.NewGuid(),null),
                new Company("Company3","TradeName" ,"00560551000100", GetAddress(), Guid.NewGuid(), null,Guid.NewGuid(),null),
                new Company("Company4","TradeName" ,"00560551000100", GetAddress(), Guid.NewGuid(), null,Guid.NewGuid(),null),
                new Company("Company5","TradeName" ,"00560551000100", GetAddress(), Guid.NewGuid(), null,Guid.NewGuid(),null)
            };
        }

        public static PagedData<Company> GetPaged()
        {
            return new PagedData<Company>() { Data = GetCompanies() };
        }
    }
}
