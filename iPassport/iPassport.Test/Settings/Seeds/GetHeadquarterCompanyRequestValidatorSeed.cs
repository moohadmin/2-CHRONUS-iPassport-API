using iPassport.Api.Models.Requests.Company;
using iPassport.Domain.Enums;
using System;

namespace iPassport.Test.Settings.Seeds
{
    public static class GetHeadquarterCompanyRequestValidatorSeed
    {
        public static GetHeadquarterCompanyRequest GetHeadquarterCompanyRequestCnpjNull() =>
            new()
            {
                Cnpj = null,
                LocalityId = Guid.NewGuid(),
                CompanyTypeIdentifyer = ECompanyType.Private,
                SegmentIdentifyer = ECompanySegmentType.Contractor
            };

        public static GetHeadquarterCompanyRequest GetHeadquarterCompanyRequestCnpjEmpty() =>
            new()
            {
                Cnpj = "",
                LocalityId = Guid.NewGuid(),
                CompanyTypeIdentifyer = ECompanyType.Private,
                SegmentIdentifyer = ECompanySegmentType.Contractor
            };

        public static GetHeadquarterCompanyRequest GetHeadquarterCompanyRequestCompanyTypeIdentifyerNull() =>
            new()
            {
                Cnpj = "00000000",
                LocalityId = Guid.NewGuid(),
                CompanyTypeIdentifyer = null,
                SegmentIdentifyer = ECompanySegmentType.Contractor
            };

        public static GetHeadquarterCompanyRequest GetHeadquarterCompanyRequestSegmentIdentifyerNull() =>
            new()
            {
                Cnpj = "00000000",
                LocalityId = Guid.NewGuid(),
                CompanyTypeIdentifyer = ECompanyType.Private,
                SegmentIdentifyer = null
            };

        public static GetHeadquarterCompanyRequest GetHeadquarterCompanyRequestLocalityIdNull() =>
            new()
            {
                Cnpj = "00000000",
                LocalityId = null,
                CompanyTypeIdentifyer = ECompanyType.Government,
                SegmentIdentifyer = ECompanySegmentType.Municipal
            };

        public static GetHeadquarterCompanyRequest GetHeadquarterCompanyRequestCnpjInvalid() =>
            new()
            {
                Cnpj = "000000000",
                LocalityId = null,
                CompanyTypeIdentifyer = ECompanyType.Private,
                SegmentIdentifyer = ECompanySegmentType.Contractor
            };
    }
}
