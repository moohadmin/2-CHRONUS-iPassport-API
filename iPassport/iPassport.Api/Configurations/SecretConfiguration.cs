using iPassport.Application.Services.Constants;

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