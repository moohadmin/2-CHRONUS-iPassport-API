namespace iPassport.Api.Models.Requests.Shared
{
    /// <summary>
    /// Get By Name Parts Paged Request
    /// </summary>
    public class GetByNamePartsPagedRequest : PageFilterRequest
    {
        /// <summary>
        /// Name Initals / Name Parts
        /// </summary>
        public string Initials { get; set; }
    }
}
