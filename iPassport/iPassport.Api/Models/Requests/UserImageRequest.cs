namespace iPassport.Api.Models.Requests
{
    public class UserImageRequest
    {
        //public string FileName { get; set; }
        //public System.Guid UserId { get; set; }
        public Microsoft.AspNetCore.Http.IFormFile ImageFile { get; set; }
    }
}
