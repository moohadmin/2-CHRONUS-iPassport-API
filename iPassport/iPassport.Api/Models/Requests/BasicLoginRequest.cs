namespace iPassport.Api.Models.Requests
{
    public class BasicLoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Document { get; set; }
    }
}