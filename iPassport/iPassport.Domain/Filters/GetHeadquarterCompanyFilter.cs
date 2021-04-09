using System;

namespace iPassport.Domain.Filters
{
    public class GetHeadquarterCompanyFilter
    {
        public string Cnpj { get; set; }
        public Guid? SegmentId { get; set; }
        public Guid? StateId { get; set; }
        public Guid? CityId { get; set; }
    }
}
