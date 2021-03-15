using AutoMapper;
using iPassport.Api.Models.Requests;
using iPassport.Application.Models.ViewModels;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;

namespace iPassport.Api.AutoMapper.Mappers
{
    public static class AddressMapper
    {
        public static void Map(Profile profile)
        {
            profile.CreateMap<AddressCreateRequest, AddressCreateDto>()
            .ForMember(des => des.Cep, act => act.MapFrom(src => src.Cep))
            .ForMember(des => des.Description, act => act.MapFrom(src => src.Description))
            .ForMember(des => des.CityId, act => act.MapFrom(src => src.CityId));

            profile.CreateMap<Address, AddressViewModel>()
            .ForMember(des => des.Cep, act => act.MapFrom(src => src.Cep))
            .ForMember(des => des.Description, act => act.MapFrom(src => src.Description))
            .ForMember(des => des.CityId, act => act.MapFrom(src => src.CityId))
            .ForMember(des => des.City, act => act.MapFrom(src => src.City != null ? src.City.Name : null))
            .ForMember(des => des.State, act => act.MapFrom(src => src.City != null && src.City.State != null ? src.City.State.Name : null))
            .ForMember(des => des.Country, act => act.MapFrom(src => src.City != null && src.City.State != null && src.City.State.Country != null ? src.City.State.Country.Name : null))
            .ForMember(des => des.StateId, opt => {
                opt.PreCondition(src => (src?.City?.State != null));
                opt.MapFrom(src => src.City.StateId);
            })
            .ForMember(des => des.CountryId, opt => {
                opt.PreCondition(src => (src?.City?.State?.Country != null));
                opt.MapFrom(src => src.City.State.CountryId);
            });
            
        }
    }
}
