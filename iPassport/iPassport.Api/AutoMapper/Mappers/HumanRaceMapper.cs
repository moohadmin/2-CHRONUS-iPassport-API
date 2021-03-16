using AutoMapper;
using iPassport.Application.Models.ViewModels;
using iPassport.Domain.Entities;

namespace iPassport.Api.AutoMapper.Mappers
{
    /// <summary>
    /// Human Race Mapper Class
    /// </summary>
    public static class HumanRaceMapper
    {
        /// <summary>
        /// Human Race Map Method
        /// </summary>
        /// <param name="profile">Auto Mapper profile</param>
        public static void Map(Profile profile)
        {
            profile.CreateMap<HumanRace, HumanRaceViewModel>()
                .ForMember(des => des.Id, act => act.MapFrom(src => src.Id))
                .ForMember(des => des.Name, act => act.MapFrom(src => src.Name));
        }
    }
}
