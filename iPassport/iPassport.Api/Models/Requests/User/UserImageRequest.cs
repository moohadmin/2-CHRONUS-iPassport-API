using Microsoft.AspNetCore.Http;

namespace iPassport.Api.Models.Requests
{
    /// <summary>
    /// User Image Request
    /// </summary>
    public class UserImageRequest
    {
        /// <summary>
        /// Image File
        /// </summary>
        public IFormFile ImageFile { get; set; }
    }
}
