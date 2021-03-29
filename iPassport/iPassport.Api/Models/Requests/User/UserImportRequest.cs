using Microsoft.AspNetCore.Http;

namespace iPassport.Api.Models.Requests.User
{
    /// <summary>
    /// User Import Request
    /// </summary>
    public class UserImportRequest
    {
        /// <summary>
        /// File
        /// </summary>
        public IFormFile File { get; set; }
    }
}
