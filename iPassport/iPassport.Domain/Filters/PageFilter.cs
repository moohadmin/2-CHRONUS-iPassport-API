namespace iPassport.Domain.Filters
{
    public class PageFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public PageFilter()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }
        public PageFilter(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize < 1 ? 10 : pageSize;
        }
    }
}
