using AutoMapper;
using iPassport.Api.Models.Requests;
using iPassport.Application.Models.ViewModels;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;

namespace iPassport.Api.AutoMapper.Mappers
{
    public static class PassportMapper
    {
        public static void Map(Profile profile)
        {
            profile.CreateMap<Passport, PassportViewModel>()
                .ForMember(des => des.PassId, act => act.MapFrom(src => src.GetPassId()))
                .ForMember(des => des.UserFullname, act => act.MapFrom(src => src.UserDetails.FullName))
                .ForMember(des => des.ExpirationDate, act => act.MapFrom(src => src.GetLastPassportDetails().ExpirationDate))
                .ForMember(des => des.PassportDetailsId, act => act.MapFrom(src => src.GetLastPassportDetails().Id));

            profile.CreateMap<PassportUseCreateRequest, PassportUseCreateDto>()
                .ForMember(des => des.Longitude, act => act.MapFrom(src => src.Longitude.ToString()))
                .ForMember(des => des.Latitude, act => act.MapFrom(src => src.Latitude.ToString()))
                .ForMember(des => des.PassportDetailsId, act => act.MapFrom(src => src.PassportDetailsId));
        }
    }
}
