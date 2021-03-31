using AutoM = AutoMapper;
using iPassport.Api.Models.Requests;
using iPassport.Application.Models.ViewModels;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;

namespace iPassport.Api.AutoMapper.Mappers
{
    /// <summary>
    /// Passport Mapper Class
    /// </summary>
    public static class PassportMapper
    {
        /// <summary>
        /// Map Method
        /// </summary>
        /// <param name="profile">Auto Mapper Profile Instance</param>
        public static void Map(AutoM.Profile profile)
        {
            profile.CreateMap<Passport, PassportViewModel>()
                .ForMember(des => des.PassId, act => act.MapFrom(src => src.GetPassId()))
                .ForMember(des => des.ExpirationDate, act => act.MapFrom(src => src.GetLastPassportDetails().ExpirationDate))
                .ForMember(des => des.PassportDetailsId, act => act.MapFrom(src => src.GetLastPassportDetails().Id));

            profile.CreateMap<PassportUseCreateRequest, PassportUseCreateDto>()
                .ForMember(des => des.Longitude, act => act.MapFrom(src => src.Longitude.ToString()))
                .ForMember(des => des.Latitude, act => act.MapFrom(src => src.Latitude.ToString()))
                .ForMember(des => des.PassportDetailsId, act => act.MapFrom(src => src.PassportDetailsId));

            profile.CreateMap<Passport, PassportToValidateViewModel>()
                .ForMember(des => des.PassId, act => act.MapFrom(src => src.GetPassId()))
                .ForMember(des => des.ExpirationDate, act => act.MapFrom(src => src.GetLastPassportDetails().ExpirationDate))
                .ForMember(des => des.PassportDetailsId, act => act.MapFrom(src => src.GetLastPassportDetails().Id));
        }
    }
}
