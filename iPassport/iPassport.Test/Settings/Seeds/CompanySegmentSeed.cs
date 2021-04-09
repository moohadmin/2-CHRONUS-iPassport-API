using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Enums;
using System;
using System.Collections.Generic;

namespace iPassport.Test.Seeds
{
    public static class CompanySegmentSeed
    {
        public static CompanySegment Get() => new("Municipal", (int)ECompanySegmentType.Municipal, Guid.NewGuid());

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
