using iPassport.Application.Models.ViewModels;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using AutoM = AutoMapper;

namespace iPassport.Api.AutoMapper.Mappers
{
    /// <summary>
    /// Profile Mapper Class
    /// </summary>
    public static class ProfileMapper
    {
        /// <summary>
        /// Map Method
        /// </summary>
        /// <param name="profile">Auto Mapper Profile Instance</param>
        public static void Map(AutoM.Profile profile)
        {
            profile.CreateMap<Profile, ProfileViewModel>();
            profile.CreateMap<ProfileDto, ProfileViewModel>();
        }
    }
}
