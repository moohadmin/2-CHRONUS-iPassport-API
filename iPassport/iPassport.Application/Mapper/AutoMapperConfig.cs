using AutoMapper;

namespace iPassport.Application.Mapper
{
    public class AutoMapperConfig
    {
        public AutoMapperConfig() { }

        public static MapperConfiguration RegisterMapper()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
        }
    }
}
