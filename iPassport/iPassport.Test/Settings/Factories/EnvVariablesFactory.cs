using System;

namespace iPassport.Test.Settings.Factories
{
    public static class EnvVariablesFactory
    {
        public static void Create()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
            Environment.SetEnvironmentVariable("NOTIFICATIONS_MOCK", "true");
            Environment.SetEnvironmentVariable("DATABASE_CONNECTION_STRING", "Host=localhost;Database=passport;Username=passport;Password=1234");
            
            Environment.SetEnvironmentVariable("STORAGE_S3_BUCKET_NAME", "chronus-docs");
            Environment.SetEnvironmentVariable("AWS_ACCESS_KEY_ID", "*****");
            Environment.SetEnvironmentVariable("AWS_SECRET_ACCESS_KEY", "*****");
            Environment.SetEnvironmentVariable("AWS_DEFAULT_REGION", "sa-east-1");

            Environment.SetEnvironmentVariable("NOTIFICATIONS_BASE_URL", "https://******.api.infobip.com");
            Environment.SetEnvironmentVariable("NOTIFICATIONS_CLIENT_ID", "*****");
            Environment.SetEnvironmentVariable("NOTIFICATIONS_CLIENT_SECRET", "*****");
            Environment.SetEnvironmentVariable("NOTIFICATIONS_GRANT_TYPE", "client_credentials");
            Environment.SetEnvironmentVariable("NOTIFICATIONS_AUTHENTICATION_KEY", "*****");
            Environment.SetEnvironmentVariable("NOTIFICATIONS_FROM_NUMBER", "IPassport");
            Environment.SetEnvironmentVariable("NOTIFICATIONS_GET_TOKEN", "/auth/1/oauth2/token");
            Environment.SetEnvironmentVariable("NOTIFICATIONS_SEND_API_URL", "/sms/2/text/advanced");
            Environment.SetEnvironmentVariable("NOTIFICATIONS_GET_API_URL", "/sms/1/reports");

            Environment.SetEnvironmentVariable("SECRET_JWT_TOKEN", "*****");

        }
    }
}
