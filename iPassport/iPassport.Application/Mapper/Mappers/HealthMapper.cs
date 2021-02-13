using AutoMapper;
using iPassport.Application.Models.ViewModels;
using iPassport.Domain.Entities;

namespace iPassport.Application.Mapper.Mappers
{
    public static class HealthMapper
    {
        /// <summary>
        /// mapping rules InstallmentTypeMapper
        /// </summary>
        /// <param name="profile"></param>
        public static void Map(Profile profile)
        {
            profile.CreateMap<Health, HealthViewModel>()
                .ForMember(des => des.HealthId, act => act.MapFrom(src => src.Id))
                .ForMember(des => des.Status, act => act.MapFrom(src => src.Status))
                .ForMember(des => des.CreateDate, act => act.MapFrom(src => src.CreateDate))
                .ForMember(des => des.UpdateDate, act => act.MapFrom(src => src.UpdateDate));
        }
    }
}
