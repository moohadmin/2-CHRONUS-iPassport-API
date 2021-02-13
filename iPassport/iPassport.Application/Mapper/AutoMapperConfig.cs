using AutoMapper;
using iPassport.Application.Mapper.Mappers;

namespace iPassport.Application.Mapper
{
    public class AutoMapperConfig
    {
        protected AutoMapperConfig() { }

        public static MapperConfiguration RegisterMapper()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new HealthMapper());
            });
        }
    }
}
