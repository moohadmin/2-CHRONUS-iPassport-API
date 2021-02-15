using AutoMapper;
using iPassport.Application.Mapper.Mappers;

namespace iPassport.Application.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            HealthMapper.Map(this);
        }
    }
}
