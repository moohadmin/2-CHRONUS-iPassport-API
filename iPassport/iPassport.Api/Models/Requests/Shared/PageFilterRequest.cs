namespace iPassport.Api.Models.Requests
{
    /// <summary>
    /// Page Filter Request
    /// </summary>
    public class PageFilterRequest
    {
        /// <summary>
        /// Page Number
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Page Size
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Class constructor
        /// </summary>
        public PageFilterRequest()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="pageNumber">Page Number</param>
        /// <param name="pageSize">Page Size</param>
        public PageFilterRequest(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize > 10 ? 10 : pageSize;
        }
    }
}
