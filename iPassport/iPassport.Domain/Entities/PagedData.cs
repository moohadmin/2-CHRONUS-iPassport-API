namespace iPassport.Domain.Entities
{
    public class PagedData<T> where T : class 
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }

        public T Data { get; set; }
    }
}
