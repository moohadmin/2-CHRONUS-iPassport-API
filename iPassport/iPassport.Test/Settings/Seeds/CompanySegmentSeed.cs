using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Enums;
using iPassport.Test.Settings.Seeds;
using System;
using System.Collections.Generic;

namespace iPassport.Test.Seeds
{
    public static class CompanySegmentSeed
    {
        public static CompanySegment GetMunicipalType()
        {
            var segment = new CompanySegment("Municipal", (int)ECompanySegmentType.Municipal, Guid.NewGuid());
            segment.CompanyType = CompanyTypeSeed.GetGovernment();
            return segment;
        }
        public static CompanySegment GetHealthType()
        {
            var segment = new CompanySegment("Saúde", (int)ECompanySegmentType.Health, Guid.NewGuid());
            segment.CompanyType = CompanyTypeSeed.GetPrivate();
            return segment;
        }
        public static IList<CompanySegment> GetAll()
        {
            return new List<CompanySegment>()
            {
                new("Municipal", (int)ECompanySegmentType.Municipal, Guid.NewGuid()),
                new("Estadual", (int)ECompanySegmentType.State, Guid.NewGuid()),
                new("Federal", (int)ECompanySegmentType.Federal, Guid.NewGuid()),
                new("Contratante", (int)ECompanySegmentType.Contractor, Guid.NewGuid()),
                new("Saúde", (int)ECompanySegmentType.Health, Guid.NewGuid())
            };
        }

        public static PagedData<CompanySegment> GetPaged() => new PagedData<CompanySegment>() { Data = GetAll() };
    }
}
