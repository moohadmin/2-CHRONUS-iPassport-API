using System;

namespace iPassport.Test.Seeds
{
    public static class EnvVariablesFactory
    {
        public static void Create()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
            Environment.SetEnvironmentVariable("PIN_INTEGRATION_SIMULADO", "true");

        }
    }
}
