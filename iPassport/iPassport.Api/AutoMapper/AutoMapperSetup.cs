using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace iPassport.Api.AutoMapper
{
    /// <summary>
    /// Auto Mapper Setup Class
    /// </summary>
    public static class AutoMapperSetup
    {
        /// <summary>
        /// Add Auto Mapper Setup Method
        /// </summary>
        /// <param name="services">Service Collection</param>
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            var config = AutoMapperConfig.RegisterMapper();

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
