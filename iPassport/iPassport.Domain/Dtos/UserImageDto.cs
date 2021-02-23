using Microsoft.AspNetCore.Http;

namespace iPassport.Domain.Dtos
{
    public class UserImageDto
    {
        public string FileName{ get; set;}
        public System.Guid UserId { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
