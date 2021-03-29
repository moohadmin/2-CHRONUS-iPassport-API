using System;

namespace iPassport.Domain.Filters
{
    public class GetByIdPagedFilter : PageFilter
    {
        public GetByIdPagedFilter() { }
        public GetByIdPagedFilter(Guid id, PageFilter pageFilter)
        {
            Id = id;
            PageNumber = pageFilter.PageNumber;
            PageSize = pageFilter.PageSize;
        }

        public Guid Id { get; set; }
    }
}
