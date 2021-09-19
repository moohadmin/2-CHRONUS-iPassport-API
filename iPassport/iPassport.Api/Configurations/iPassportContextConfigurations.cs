using iPassport.Application.Services.Constants;
using iPassport.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace iPassport.Api.Configurations
{
    /// <summary>
    /// iPassport Context Configurations
    /// </summary>
    public static class iPassportContextConfigurations
    {
        /// <summary>
        /// Create LoggerFactory
        /// </summary>
        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        /// <summary>
        /// Add Custom Data Context
        /// </summary>
        /// <param name="services">Service Collection</param>
        /// <returns>Service Collection</returns>
        public static IServiceCollection AddCustomDataContext(this IServiceCollection services)
        {
            string connection = EnvConstants.DATABASE_CONNECTION_STRING;

            services.AddScoped((provider) =>
            {
                return new DbContextOptionsBuilder<iPassportContext>()
                .UseNpgsql(connection)
                .Options;
            });
#if (DEBUG)
            services.AddDbContext<iPassportContext>(opt => opt
                .UseLoggerFactory(MyLoggerFactory)
                .EnableSensitiveDataLogging(true)
                .EnableDetailedErrors());
#else
            services.AddDbContext<iPassportContext>(opt => opt
                .UseLoggerFactory(MyLoggerFactory)
                .EnableDetailedErrors());
#endif

            return services;
        }
    }
}
