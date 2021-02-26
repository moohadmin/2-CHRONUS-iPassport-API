using iPassport.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace iPassport.Api.Configurations
{
    public static class iPassportContextConfigurations
    {
        /// <summary>
        /// Create LoggerFactory
        /// </summary>
        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public static IServiceCollection AddCustomDataContext(this IServiceCollection services, IConfiguration configuration)
        {
            string connection = System.Environment.GetEnvironmentVariable("CONNECTION_STRING");
            //string connection = configuration.GetConnectionString("DefaultConnection");

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
                .EnableSensitiveDataLogging(true)
                .EnableDetailedErrors());
#endif

            return services;
        }
    }
}
