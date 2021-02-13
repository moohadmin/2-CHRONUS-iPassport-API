using iPassport.Application.Interfaces;
using iPassport.Application.Services;
using iPassport.Domain.Entities;
using iPassport.Domain.Repositories;
using iPassport.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace iPassport.Api.Configurations
{
    /// Dependency Injection Class
    public static class DependencyInjection
    {
        /// Dependency Injection Register
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            #region DI Services
            services.AddScoped<IHealthService, HealthService>();
            #endregion

            #region DI Repositories
            services.AddScoped<IRepository<Entity>, Repository<Entity>>();
            services.AddScoped<IHealthRepository, HealthRepository>();
            #endregion

            return services;
        }
    }
}
