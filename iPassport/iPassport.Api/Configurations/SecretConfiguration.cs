using Microsoft.Extensions.Configuration;

namespace iPassport.Api.Configurations
{
    public static class SecretConfiguration
    {
        public static string GetSecret(IConfiguration _configuration)
        {
            return _configuration.GetSection("Secret").Value;
        }
    }
}