using iPassport.Application.Interfaces;
using iPassport.Application.Interfaces.Authentication;
using iPassport.Application.Middlewares.Auth;
using iPassport.Application.Services;
using iPassport.Application.Services.AuthenticationServices;
using iPassport.Domain.Entities;
using iPassport.Domain.Repositories;
using iPassport.Domain.Repositories.Authentication;
using iPassport.Domain.Repositories.PassportIdentityContext;
using iPassport.Infra.ExternalServices;
using iPassport.Infra.ExternalServices.StorageExternalServices;
using iPassport.Infra.Repositories;
using iPassport.Infra.Repositories.AuthenticationRepositories;
using iPassport.Infra.Repositories.IdentityContext;
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

            services.AddTransient<TokenManagerMiddleware>();

            services.AddScoped<IAccountService, AccountService>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<ISmsExternalService, SmsIntegrationService>();

            services.AddScoped<IAuth2FactService, Auth2FactService>();

            services.AddScoped<IPlanService, PlanService>();

            services.AddScoped<IPassportService, PassportService>();

            services.AddScoped<IUserVaccineService, UserVaccineService>();

            services.AddScoped<IDiseaseService, DiseaseService>();

            services.AddScoped<IVaccineService, VaccineService>();

            services.AddScoped<IVaccineManufacturerService, VaccineManufacturerService>();

            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<IStorageExternalService, StorageExternalService>();

            services.AddScoped<ICountryService, CountryService>();

            services.AddScoped<IStateService, StateService>();
            
            services.AddScoped<ICityService, CityService>();

            services.AddScoped<ICompanyService, CompanyService>();

            services.AddScoped<IHealthUnitService, HealthUnitService>();
            
            services.AddScoped<IGenderService, GenderService>();

            services.AddScoped<IUserDiseaseTestService, UserDiseaseTestService>();

            services.AddScoped<IPriorityGroupService, PriorityGroupService>();

            services.AddScoped<IBloodTypeService, BloodTypeService>();

            services.AddScoped<IHumanRaceService, HumanRaceService>();

            services.AddScoped<IImportedFileService, ImportedFileService>();


            services.AddScoped<IHealthUnitTypeService, HealthUnitTypeService>();
            #endregion

            #region DI Repositories
            services.AddScoped<IRepository<Entity>, Repository<Entity>>();

            services.AddScoped<IHealthRepository, HealthRepository>();

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IUserDetailsRepository, UserDetailsRepository>();

            services.AddScoped<IPlanRepository, PlanRepository>();

            services.AddScoped<IAuth2FactMobileRepository, Auth2FactMobileRepository>();

            services.AddScoped<IPassportRepository, PassportRepository>();

            services.AddScoped<IUserVaccineRepository, UserVaccineRepository>();

            services.AddScoped<IPassportUseRepository, PassportUseRepository>();

            services.AddScoped<IPassportDetailsRepository, PassportDetailsRepository>();

            services.AddScoped<IDiseaseRepository, DiseaseRepository>();

            services.AddScoped<IVaccineManufacturerRepository, VaccineManufacturerRepository>();

            services.AddScoped<IVaccineRepository, VaccineRepository>();

            services.AddScoped<IAddressRepository, AddressRepository>();
            
            services.AddScoped<ICityRepository, CityRepository>();

            services.AddScoped<IStateRepository, StateRepository>();

            services.AddScoped<ICountryRepository, CountryRepository>();

            services.AddScoped<IUserTokenRepository, UserTokenRepository>();

            services.AddScoped<ICompanyRepository, CompanyRepository>();

            services.AddScoped<IHealthUnitRepository, HealthUnitRepository>();

            services.AddScoped<IGenderRepository, GenderRepository>();

            services.AddScoped<IUserDiseaseTestRepository, UserDiseaseTestRepository>();

            services.AddScoped<IPriorityGroupRepository, PriorityGroupRepository>();

            services.AddScoped<IBloodTypeRepository, BloodTypeRepository>();

            services.AddScoped<IHumanRaceRepository, HumanRaceRepository>();

            services.AddScoped<IHealthUnitTypeRepository, HealthUnitTypeRepository>();

            services.AddScoped<IImportedFileDetailsRepository, ImportedFileDetailsRepository>();

            services.AddScoped<IImportedFileRepository, ImportedFileRepository>();
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IProfileRepository, ProfileRepository>();
            #endregion

            #region DI Settings

            /// ASP.NET HttpContext dependency
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            #endregion

            return services;
        }
    }
}

