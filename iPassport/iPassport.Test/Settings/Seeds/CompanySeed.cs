using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Enums;
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
        public static IList<Company> GetCompanies(bool active = true)
        {
            var company2 = new Company("Company2", "TradeName", "00560551000100", GetAddress(), Guid.NewGuid(), null, Guid.NewGuid(), null);
            var company3 = new Company("Company3", "TradeName", "00560551000100", GetAddress(), Guid.NewGuid(), null, Guid.NewGuid(), null);
            var company4 = new Company("Company4", "TradeName", "00560551000100", GetAddress(), Guid.NewGuid(), null, Guid.NewGuid(), null);
            var company5 = new Company("Company5", "TradeName", "00560551000100", GetAddress(), Guid.NewGuid(), null, Guid.NewGuid(), null);
            if (!active)
            {
                company2.Deactivate(Guid.NewGuid());
                company3.Deactivate(Guid.NewGuid());
                company4.Deactivate(Guid.NewGuid());
                company5.Deactivate(Guid.NewGuid());
            }
            return new List<Company>()
            {
               company2,
               company3,
               company4,
               company5
            };
        }

        public static PagedData<Company> GetPaged()
        {
            return new PagedData<Company>() { Data = GetCompanies() };
        }
        
        public static Company Get(bool? Isheadquarter, bool? isActive, ECompanySegmentType? segment = null, string cnpj = "01948981000166", string name = "Company1", bool? hasAddress=true)
        {
            if (!Isheadquarter.HasValue && !isActive.HasValue && segment == null)
                return null;

            var company = new Company(name, "TradeName", cnpj, GetAddress(), Guid.NewGuid(), Isheadquarter, Guid.NewGuid(), null);
            if(segment != null)
            {
                company.Segment = segment.Value switch
                {
                    ECompanySegmentType.Municipal => CompanySegmentSeed.GetMunicipalType(),
                    ECompanySegmentType.Health => CompanySegmentSeed.GetHealthType(),
                    ECompanySegmentType.Contractor => CompanySegmentSeed.GetContractorType(),
                    ECompanySegmentType.Federal => CompanySegmentSeed.GetFederalType(),
                    ECompanySegmentType.State => CompanySegmentSeed.GetStateType(),
                    _ => null,
                };
            }
            
            if (!isActive.GetValueOrDefault())
                company.Deactivate(Guid.NewGuid());

            if (hasAddress.GetValueOrDefault())
                company.Address = AddressSeed.GetLoaded();
            else
                company.Address = null;

            return company;
        }
        

    }
}
