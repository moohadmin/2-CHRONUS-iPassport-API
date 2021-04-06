using System;

namespace iPassport.Api.Models.Requests.Shared
{
    /// <summary>
    /// Get By Id Paged Request
    /// </summary>
    public class GetByIdPagedRequest : PageFilterRequest
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
    }
}
