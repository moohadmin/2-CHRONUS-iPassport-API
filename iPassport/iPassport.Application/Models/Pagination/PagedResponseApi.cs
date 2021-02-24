namespace iPassport.Application.Models.Pagination
{
    public class PagedResponseApi : ResponseApi
    {
        public PagedResponseApi(bool success, string message, int pageNumber, int pageSize, int totalPages, int totalRecords, object data = null)
            :base(success,message, data)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = totalPages;
            TotalRecords = totalRecords;
        }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
    }
}
