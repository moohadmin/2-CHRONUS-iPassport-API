using iPassport.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace iPassport.Api.Configurations
{
    public static class iPassportContextConfigurations
    {
        public static IServiceCollection AddCustomDataContext(this IServiceCollection services, IConfiguration configuration)
        {
            string connection = configuration.GetConnectionString("DefaultConnection");

            services.AddScoped((provider) =>
            {
                return new DbContextOptionsBuilder<iPassportContext>()
                .UseSqlServer(connection)
                .Options;
            });

            services.AddDbContext<iPassportContext>();

            return services;
        }
    }
}
