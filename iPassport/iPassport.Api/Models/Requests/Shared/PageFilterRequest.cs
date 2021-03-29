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
    }
}
