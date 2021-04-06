using iPassport.Application.Services.Constants;

namespace iPassport.Api.Configurations
{
    /// <summary>
    /// Secret Configuration Class
    /// </summary>
    public static class SecretConfiguration
    {
        /// <summary>
        /// Get Secret Method
        /// </summary>
        /// <returns>secret jwt constant</returns>
        public static string GetSecret()
        {
            return EnvConstants.SECRET_JWT_TOKEN;
        }
    }
}