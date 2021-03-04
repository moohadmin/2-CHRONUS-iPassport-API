using System;

namespace iPassport.Domain.Filters
{
    public class GetByIdPagedFilter : PageFilter
    {
        public Guid Id { get; set; }
    }
}
