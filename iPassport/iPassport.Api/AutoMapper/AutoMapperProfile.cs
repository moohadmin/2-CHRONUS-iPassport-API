using AutoMapper;
using iPassport.Api.AutoMapper.Mappers;

namespace iPassport.Api.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            HealthMapper.Map(this);
            UserMapper.Map(this);
            PlanMapper.Map(this);
            PassportMapper.Map(this);
        }
    }
}
