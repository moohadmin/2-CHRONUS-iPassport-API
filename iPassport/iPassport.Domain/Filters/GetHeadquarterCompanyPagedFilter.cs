using System;

namespace iPassport.Domain.Filters
{
    public class GetHeadquarterCompanyPagedFilter : PageFilter
    {
        public string Initials { get; set; }
        public Guid? SegmentId { get; set; }
        public Guid? StateId { get; set; }
    }
}
