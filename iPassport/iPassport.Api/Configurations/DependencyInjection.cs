using iPassport.Application.Interfaces;
using iPassport.Application.Services;
using iPassport.Application.Services.AuthenticationServices;
using iPassport.Domain.Entities;
using iPassport.Domain.Repositories;
using iPassport.Infra.ExternalServices;
using iPassport.Infra.Repositories;
using Microsoft.AspNetCore.Http;
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

            services.AddScoped<ITokenService, TokenService>();
            
            services.AddScoped<IAccountService, AccountService>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<ISmsExternalService, SmsIntegrationService>();

            services.AddScoped<IAuth2FactService, Auth2FactService>();
            
            services.AddScoped<IPlanService, PlanService>();
            
            services.AddScoped<IVaccineService, VaccineService>();

            #endregion

            #region DI Repositories
            services.AddScoped<IRepository<Entity>, Repository<Entity>>();

            services.AddScoped<IHealthRepository, HealthRepository>();

            services.AddScoped<IUserDetailsRepository, UserDetailsRepository>();

            services.AddScoped<IPlanRepository, PlanRepository>();

            services.AddScoped<IVaccineRepository, VaccineRepository>();

            #endregion

            #region DI Settings

            // ASP.NET HttpContext dependency
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            #endregion

            return services;
        }
    }
}
