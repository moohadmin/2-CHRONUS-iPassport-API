using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Enums;
using System;
using System.Collections.Generic;

namespace iPassport.Test.Seeds
{
    public static class CompanySegmentSeed
    {
        public static CompanySegment Get() => new("Municipal", (int)ECompanySegment.Municpial, Guid.NewGuid());

        public static IList<CompanySegment> GetAll()
        {
            return new List<CompanySegment>()
            {
                new("Municipal", (int)ECompanySegment.Municpial, Guid.NewGuid()),
                new("Estadual", (int)ECompanySegment.State, Guid.NewGuid()),
                new("Federal", (int)ECompanySegment.Federal, Guid.NewGuid()),
                new("Contratante", (int)ECompanySegment.Contractor, Guid.NewGuid()),
                new("Saúde", (int)ECompanySegment.Health, Guid.NewGuid())
            };
        }

        public static PagedData<CompanySegment> GetPaged() => new PagedData<CompanySegment>() { Data = GetAll() };
    }
}
