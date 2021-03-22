using AutoMapper;
using iPassport.Api.Models.Requests.User;
using iPassport.Application.Models.ViewModels;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;

namespace iPassport.Api.AutoMapper.Mappers
{
    /// <summary>
    /// User Disease Test Mapper Class
    /// </summary>
    public static class UserDiseaseTestMapper
    {
        /// <summary>
        /// Map Method
        /// </summary>
        /// <param name="profile">Auto Mapper Profile Instance</param>
        public static void Map(Profile profile)
        {
            profile.CreateMap<UserDiseaseTest, UserDiseaseTestViewModel>()
                .ReverseMap();

            profile.CreateMap<UserDiseaseTestCreateRequest, UserDiseaseTestCreateDto>()
                .ForMember(des => des.Result, act => act.MapFrom(src => src.Result))
                .ForMember(des => des.TestDate, act => act.MapFrom(src => src.TestDate))
                .ForMember(des => des.ResultDate, act => act.MapFrom(src => src.ResultDate));

            profile.CreateMap<UserDiseaseTestDto, UserDiseaseTestViewModel>();

            profile.CreateMap<UserDiseaseTestEditRequest, UserDiseaseTestEditDto>()
                .ForMember(des => des.Result, act => act.MapFrom(src => src.Result))
                .ForMember(des => des.TestDate, act => act.MapFrom(src => src.TestDate))
                .ForMember(des => des.ResultDate, act => act.MapFrom(src => src.ResultDate))
                .ForMember(des => des.Id, act => act.MapFrom(src => src.Id))
                ;
        }
    }
}
