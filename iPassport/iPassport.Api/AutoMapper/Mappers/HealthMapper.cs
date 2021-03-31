using AutoM = AutoMapper;
using iPassport.Application.Models.ViewModels;
using iPassport.Domain.Entities;

namespace iPassport.Api.AutoMapper.Mappers
{
    /// <summary>
    /// Health Mapper Class
    /// </summary>
    public static class HealthMapper
    {
        /// <summary>
        /// Map Method
        /// </summary>
        /// <param name="profile">Auto Mapper Profile Instance</param>
        public static void Map(AutoM.Profile profile)
        {
            profile.CreateMap<Health, HealthViewModel>()
                .ForMember(des => des.HealthId, act => act.MapFrom(src => src.Id))
                .ForMember(des => des.Status, act => act.MapFrom(src => src.Status))
                .ForMember(des => des.CreateDate, act => act.MapFrom(src => src.CreateDate))
                .ForMember(des => des.UpdateDate, act => act.MapFrom(src => src.UpdateDate));
        }
    }
}
