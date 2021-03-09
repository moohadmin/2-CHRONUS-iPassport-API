using iPassport.Application.Services.Constants;
using iPassport.Infra.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace iPassport.Api.Configurations
{
    public static class iPassportIdentityContextConfigurations
    {
        /// <summary>
        /// Create LoggerFactory
        /// </summary>
        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public static IServiceCollection AddIdentityDataContext(this IServiceCollection services)
        {
            string connection = EnvConstants.DATABASE_CONNECTION_STRING;

            services.AddScoped((provider) =>
            {
                return new DbContextOptionsBuilder<PassportIdentityContext>()
                .UseNpgsql(connection)
                .Options;
            });
#if (DEBUG)
            services.AddDbContext<PassportIdentityContext>(opt => opt
                .UseLoggerFactory(MyLoggerFactory)
                .EnableSensitiveDataLogging(true)
                .EnableDetailedErrors());
#else
            services.AddDbContext<PassportIdentityContext>(opt => opt
                .UseLoggerFactory(MyLoggerFactory)
                .EnableSensitiveDataLogging(true)
                .EnableDetailedErrors());
#endif

            services.Configure<IdentityOptions>(options =>
            {
                // Default Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;
            });

            return services;
        }
    }
}
