using AutoMapper;
using iPassport.Application.Models.ViewModels;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;

namespace iPassport.Api.AutoMapper.Mappers
{
    /// <summary>
    /// Gender Mapper Class
    /// </summary>
    public static class GenderMapper
    {
        /// <summary>
        /// Gender Map Method
        /// </summary>
        /// <param name="profile">Auto Mapper profile</param>
        public static void Map(Profile profile)
        {
            profile.CreateMap<Gender, GenderViewModel>()
                .ForMember(des => des.Id, act => act.MapFrom(src => src.Id))
                .ForMember(des => des.Name, act => act.MapFrom(src => src.Name))
                .ReverseMap();

            profile.CreateMap<GenderDto, GenderViewModel>();
        }
    }
}
