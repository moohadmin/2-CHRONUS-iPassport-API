using System;

namespace iPassport.Domain.Filters
{
    public class GetByIdAndNamePartsPagedFilter : PageFilter
    {
        public string Initials { get; set; }
        public Guid Id { get; set; }
    }
}
