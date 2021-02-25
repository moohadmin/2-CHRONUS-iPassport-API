namespace iPassport.Infra.ExternalServices
{
    public class SmsTokenModel
    {
        public string clientId { get; set; }
        public string clientSecret { get; set; }
        public string grantType { get; set; }
    }

    public class SmsTokenResponseModel
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
    }
}