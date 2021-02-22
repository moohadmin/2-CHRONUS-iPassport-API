using iPassport.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace iPassport.Api.Configurations
{
    public static class iPassportIdentityContextConfigurations
    {
        /// <summary>
        /// Create LoggerFactory
        /// </summary>
        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public static IServiceCollection AddIdentityDataContext(this IServiceCollection services, IConfiguration configuration)
        {
            string connection = Environment.GetEnvironmentVariable("CONNECTION_STRING");

            services.AddScoped((provider) =>
            {
                return new DbContextOptionsBuilder<PassportIdentityContext>()
                .UseSqlServer(connection)
                .Options;
            });
#if (DEBUG)
            services.AddDbContext<PassportIdentityContext>(opt => opt
                .UseLoggerFactory(MyLoggerFactory)
                .EnableSensitiveDataLogging(true)
                .EnableDetailedErrors());
#else
            services.AddDbContext<PassportIdentityContext>();
#endif

            return services;
        }
    }
}
