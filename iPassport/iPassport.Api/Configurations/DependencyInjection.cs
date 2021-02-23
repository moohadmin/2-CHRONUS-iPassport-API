using iPassport.Application.Interfaces;
using iPassport.Application.Services;
using iPassport.Application.Services.AuthenticationServices;
using iPassport.Domain.Entities;
using iPassport.Domain.Repositories;
using iPassport.Domain.Repositories.Authentication;
using iPassport.Infra.ExternalServices;
using iPassport.Infra.Repositories;
using iPassport.Infra.Repositories.AuthenticationRepositories;
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

            services.AddScoped<IExternalStorageService, ExternalStorageService>();

            services.AddScoped<ISmsExternalService, SmsIntegrationService>();

            services.AddScoped<IAuth2FactService, Auth2FactService>();

            services.AddScoped<IPlanService, PlanService>();

            #endregion

            #region DI Repositories
            services.AddScoped<IRepository<Entity>, Repository<Entity>>();

            services.AddScoped<IHealthRepository, HealthRepository>();

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IUserDetailsRepository, UserDetailsRepository>();

            services.AddScoped<IPlanRepository, PlanRepository>();

            services.AddScoped<IAuth2FactMobileRepository, Auth2FactMobileRepository>();

            #endregion

            #region DI Settings

            // ASP.NET HttpContext dependency
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            #endregion

            return services;
        }
    }
}

