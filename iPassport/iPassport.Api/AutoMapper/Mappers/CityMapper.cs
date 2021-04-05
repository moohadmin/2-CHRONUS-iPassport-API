using AutoM = AutoMapper;
using iPassport.Api.Models.Requests;
using iPassport.Application.Models.ViewModels;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;

namespace iPassport.Api.AutoMapper.Mappers
{
    /// <summary>
    /// City Mapper Class
    /// </summary>
    public static class CityMapper
    {
        /// <summary>
        /// Map Method
        /// </summary>
        /// <param name="profile">Auto Mapper Profile Instance</param>
        public static void Map(AutoM.Profile profile)
        {
            profile.CreateMap<City, CityViewModel>()
            .ForMember(des => des.StateId, act => act.MapFrom(src => src.StateId))
            .ForMember(des => des.IbgeCode, act => act.MapFrom(src => src.IbgeCode))
            .ForMember(des => des.Id, act => act.MapFrom(src => src.Id))
            .ForMember(des => des.Name, act => act.MapFrom(src => src.Name))
            .ForMember(des => des.Population, act => act.MapFrom(src => src.Population));

            profile.CreateMap<CityCreateRequest, CityCreateDto>()
            .ForMember(des => des.StateId, act => act.MapFrom(src => src.StateId))
            .ForMember(des => des.IbgeCode, act => act.MapFrom(src => src.IbgeCode))
            .ForMember(des => des.Name, act => act.MapFrom(src => src.Name))
            .ForMember(des => des.Population, act => act.MapFrom(src => src.Population));

        }
    }
}
