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
        }
    }
}
