using AutoMapper;
using iPassport.Api.AutoMapper.Mappers;

namespace iPassport.Api.AutoMapper
{
    /// <summary>
    /// Auto Mapper Profile Class
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// Mapper Class Constructor
        /// </summary>
        public AutoMapperProfile()
        {
            HealthMapper.Map(this);
            UserMapper.Map(this);
            PlanMapper.Map(this);
            PassportMapper.Map(this);
            VaccineMapper.Map(this);
            PaginationMapper.Map(this);
            IndicatorMapper.Map(this);
            CountryMapper.Map(this);
            StateMapper.Map(this);
            CityMapper.Map(this);
            CompanyMapper.Map(this);
            AddressMapper.Map(this);
            HealthUnitMapper.Map(this);
            GenderMapper.Map(this);
            UserDiseaseTestMapper.Map(this);
            PriorityGroupMapper.Map(this);
        }
    }
}
