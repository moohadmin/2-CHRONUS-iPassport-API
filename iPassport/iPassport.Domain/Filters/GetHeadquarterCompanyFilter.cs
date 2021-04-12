using iPassport.Domain.Enums;
using System;

namespace iPassport.Domain.Filters
{
    public class GetHeadquarterCompanyFilter
    {
        public string Cnpj { get; set; }
        public Guid SegmentId { get; set; }
        public Guid CompanyTypeId { get; set; }
        public Guid? LocalityId { get; set; }
    }
}
