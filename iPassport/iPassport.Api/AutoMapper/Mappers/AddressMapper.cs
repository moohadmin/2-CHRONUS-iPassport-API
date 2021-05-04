using AutoM = AutoMapper;
using iPassport.Api.Models.Requests;
using iPassport.Application.Models.ViewModels;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;

namespace iPassport.Api.AutoMapper.Mappers
{
    /// <summary>
    /// Address Mapper Class
    /// </summary>
    public static class AddressMapper
    {
        /// <summary>
        /// Map Method
        /// </summary>
        /// <param name="profile">Auto Mapper Profile Instance</param>
        public static void Map(AutoM.Profile profile)
        {
            profile.CreateMap<AddressCreateRequest, AddressCreateDto>()
                .ForMember(des => des.Cep, act => act.MapFrom(src => src.Cep))
                .ForMember(des => des.Description, act => act.MapFrom(src => src.Description))
                .ForMember(des => des.CityId, act => act.MapFrom(src => src.CityId))
                .ForMember(des => des.Number, act => act.MapFrom(src => src.Number))
                .ForMember(des => des.District, act => act.MapFrom(src => src.District));

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
                })
                .ForMember(des => des.StateAcronym, opt => {
                    opt.PreCondition(src => (src?.City?.State != null));
                    opt.MapFrom(src => src.City.State.Acronym);
                });

            profile.CreateMap<AddressDto, AddressViewModel>()
                .ForMember(des => des.Cep, act => act.MapFrom(src => src.Cep))
                .ForMember(des => des.Description, act => act.MapFrom(src => src.Description))
                .ForMember(des => des.CityId, act => act.MapFrom(src => src.CityId))
                .ForMember(des => des.City, act => act.MapFrom(src => src.City != null ? src.City.Name : null))
                .ForMember(des => des.State, act => act.MapFrom(src => src.City != null && src.City.State != null ? src.City.State.Name : null))
                .ForMember(des => des.Country, act => act.MapFrom(src => src.City != null && src.City.State != null && src.City.State.Country != null ? src.City.State.Country.Name : null))
                .ForMember(des => des.StateId, opt => {
                    opt.PreCondition(src => (src?.City?.State != null));
                    opt.MapFrom(src => src.City.State.Id);
                })
                .ForMember(des => des.CountryId, opt => {
                    opt.PreCondition(src => (src?.City?.State?.Country != null));
                    opt.MapFrom(src => src.City.State.Country.Id);
                })
                .ForMember(des => des.StateAcronym, opt => {
                    opt.PreCondition(src => (src?.City?.State != null));
                    opt.MapFrom(src => src.City.State.Acronym);
                })
                .ReverseMap();

            profile.CreateMap<AddressEditRequest, AddressEditDto>()
                .ForMember(des => des.Id, act => act.MapFrom(src => src.Id))
                .ForMember(des => des.Cep, act => act.MapFrom(src => src.Cep))
                .ForMember(des => des.Description, act => act.MapFrom(src => src.Description))
                .ForMember(des => des.CityId, act => act.MapFrom(src => src.CityId))
                .ForMember(des => des.Number, act => act.MapFrom(src => src.Number))
                .ForMember(des => des.District, act => act.MapFrom(src => src.District));

            profile.CreateMap<AddressCreateRequest, AddressDto>();
        }
    }
}
