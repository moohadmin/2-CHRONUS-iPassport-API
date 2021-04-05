using AutoM = AutoMapper;
using iPassport.Api.Models.Requests;
using iPassport.Application.Models.ViewModels;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;

namespace iPassport.Api.AutoMapper.Mappers
{
    /// <summary>
    /// State Mapper Class
    /// </summary>
    public static class StateMapper
    {
        /// <summary>
        /// Map Method
        /// </summary>
        /// <param name="profile">Auto Mapper Profile Instance</param>
        public static void Map(AutoM.Profile profile)
        {
            profile.CreateMap<State, StateViewModel>()
            .ForMember(des => des.Acronym, act => act.MapFrom(src => src.Acronym))
            .ForMember(des => des.CountryId, act => act.MapFrom(src => src.CountryId))
            .ForMember(des => des.IbgeCode, act => act.MapFrom(src => src.IbgeCode))
            .ForMember(des => des.Id, act => act.MapFrom(src => src.Id))
            .ForMember(des => des.Name, act => act.MapFrom(src => src.Name))
            .ForMember(des => des.Population, act => act.MapFrom(src => src.Population));

            profile.CreateMap<StateCreateRequest, StateCreateDto>()
            .ForMember(des => des.Acronym, act => act.MapFrom(src => src.Acronym))
            .ForMember(des => des.CountryId, act => act.MapFrom(src => src.CountryId))
            .ForMember(des => des.IbgeCode, act => act.MapFrom(src => src.IbgeCode))
            .ForMember(des => des.Name, act => act.MapFrom(src => src.Name))
            .ForMember(des => des.Population, act => act.MapFrom(src => src.Population));

        }
    }
}
