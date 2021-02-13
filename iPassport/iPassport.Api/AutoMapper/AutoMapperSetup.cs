using AutoMapper;
using iPassport.Application.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace iPassport.Api.AutoMapper
{
    public static class AutoMapperSetup
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            var config = AutoMapperConfig.RegisterMapper();

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
