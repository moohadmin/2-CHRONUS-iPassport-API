namespace iPassport.Api.Models.Requests
{
    public class LoginMobileRequest
    {
        public string Pin { get; set; }
        public int DocumentType { get; set; }
        public string Document { get; set; }
    }
}