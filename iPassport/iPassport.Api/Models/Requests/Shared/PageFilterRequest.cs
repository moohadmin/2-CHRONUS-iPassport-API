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
    }
}
