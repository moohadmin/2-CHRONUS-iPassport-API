using iPassport.Api.Models.Requests.Company;
using iPassport.Domain.Enums;
using System;

namespace iPassport.Test.Settings.Seeds
{
    public static class GetHeadquarterCompanyRequestValidatorSeed
    {
        public static GetHeadquarterCompanyRequest GetHeadquarterCompanyRequestCompanyTypeIdentifyerNull() =>
            new()
            {
                Cnpj = "00000000",
                LocalityId = Guid.NewGuid(),
                CompanyTypeId = null,
                SegmentId = Guid.NewGuid()
            };

        public static GetHeadquarterCompanyRequest GetHeadquarterCompanyRequestSegmentIdentifyerNull() =>
            new()
            {
                Cnpj = "00000000",
                LocalityId = Guid.NewGuid(),
                CompanyTypeId = Guid.NewGuid(),
                SegmentId = null
            };
    }
}
