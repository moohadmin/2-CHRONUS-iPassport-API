
namespace iPassport.Api.Models.Requests
{
    public class LoginMobileRequest
    {
        public int Pin { get; set; }
        public System.Guid UserId { get; set; }
    }
}