using iPassport.Application.Services.Constants;
using Microsoft.Extensions.Configuration;

namespace iPassport.Api.Configurations
{
    public static class SecretConfiguration
    {
        public static string GetSecret()
        {
            return EnvConstants.SECRET_JWT_TOKEN;
        }
    }
}