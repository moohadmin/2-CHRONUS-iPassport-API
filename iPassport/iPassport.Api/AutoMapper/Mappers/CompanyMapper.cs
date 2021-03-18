using AutoMapper;
using iPassport.Api.Models.Requests;
using iPassport.Application.Models.ViewModels;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;

namespace iPassport.Api.AutoMapper.Mappers
{
    /// <summary>
    /// Company Mapper Class
    /// </summary>
    public static class CompanyMapper
    {
        /// <summary>
        /// Gender Map Method
        /// </summary>
        /// <param name="profile">Auto Mapper profile</param>
        public static void Map(Profile profile)
        {
            profile.CreateMap<Company, CompanyViewModel>()
                .ForMember(des => des.Name, act => act.MapFrom(src => src.Name))
                .ForMember(des => des.AddressId, act => act.MapFrom(src => src.AddressId))
                .ForMember(des => des.Id, act => act.MapFrom(src => src.Id))
                .ForMember(des => des.Cnpj, act => act.MapFrom(src => src.Cnpj));

            profile.CreateMap<CompanyCreateRequest, CompanyCreateDto>()
                .ForMember(des => des.Cnpj, act => act.MapFrom(src => src.Cnpj))
                .ForMember(des => des.Name, act => act.MapFrom(src => src.Name))
                .ForMember(des => des.AddressDto, act => act.MapFrom(src => src.Address));

            profile.CreateMap<CompanyDto, CompanyViewModel>();
        }
    }
}
