using AutoMapper;
using iPassport.Api.Models.Requests;
using iPassport.Domain.Filters;

namespace iPassport.Api.AutoMapper.Mappers
{
    public static class IndicatorMapper
    {
        public static void Map(Profile profile)
        {
            profile.CreateMap<GetVaccinatedCountRequest, GetVaccinatedCountFilter>();
            profile.CreateMap<GetByNameInitalsRequest, GetByNameInitalsFilter>();
        }
    }
}
