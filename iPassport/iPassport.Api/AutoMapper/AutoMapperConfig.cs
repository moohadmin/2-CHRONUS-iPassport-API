using AutoMapper;

namespace iPassport.Api.AutoMapper
{
    /// <summary>
    /// Auto Mapper Config Class
    /// </summary>
    public class AutoMapperConfig
    {
        /// <summary>
        /// Class Constructor
        /// </summary>
        protected AutoMapperConfig() { }

        /// <summary>
        /// Register Mapper Method
        /// </summary>
        /// <returns>Mapper Configuration</returns>
        public static MapperConfiguration RegisterMapper()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
        }
    }
}
