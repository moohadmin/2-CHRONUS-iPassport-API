using Microsoft.Extensions.DependencyInjection;

namespace iPassport.Api.Configurations
{
    /// Dependency Injection Class
    public static class DependencyInjection
    {
        /// Dependency Injection Register
        public static void RegisterDependencyInjection(IServiceCollection services) => ConfigureServiceRepository(services);

        /// Dependency Injection Configure
        public static void ConfigureServiceRepository(IServiceCollection services)
        {
            //Doing
        }
    }
}
