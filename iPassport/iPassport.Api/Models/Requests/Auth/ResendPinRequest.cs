
namespace iPassport.Api.Models.Requests
{
    public class ResendPinRequest
    {
        public string Phone { get; set; }
        public System.Guid UserId { get; set; }
    }
}