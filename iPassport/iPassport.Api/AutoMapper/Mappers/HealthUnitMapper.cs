using AutoMapper;
using iPassport.Application.Models.ViewModels;
using iPassport.Domain.Entities;

namespace iPassport.Api.AutoMapper.Mappers
{
    /// <summary>
    /// Health Unit Mapper Class
    /// </summary>
    public static class HealthUnitMapper
    {
        /// <summary>
        /// Health Unit Map Method
        /// </summary>
        /// <param name="profile">Auto Mapper profile</param>
        public static void Map(Profile profile)
        {
            profile.CreateMap<HealthUnit, HealthUnitViewModel>()
                 .ForMember(des => des.Id, act => act.MapFrom(src => src.Id))
                 .ForMember(des => des.Name, act => act.MapFrom(src => src.Name))
                 .ForMember(des => des.Cnpj, act => act.MapFrom(src => src.Cnpj))
                 .ForMember(des => des.Email, act => act.MapFrom(src => src.Email))
                 .ForMember(des => des.ResponsiblePersonName, act => act.MapFrom(src => src.ResponsiblePersonName))
                 .ForMember(des => des.ResponsiblePersonPhone, act => act.MapFrom(src => src.ResponsiblePersonPhone))
                 .ForMember(des => des.ResponsiblePersonOccupation, act => act.MapFrom(src => src.ResponsiblePersonOccupation))
                 .ForMember(des => des.IsActive, act => act.MapFrom(src => !src.DeactivationDate.HasValue))
                 .ForMember(des => des.Type, act => act.MapFrom(src => src.Type))
                 .ForMember(des => des.AddressId, act => act.MapFrom(src => src.AddressId));
            
            profile.CreateMap<HealthUnitType, HealthUnitTypeViewModel>()
                .ForMember(des => des.Id, act => act.MapFrom(src => src.Id))
                 .ForMember(des => des.Name, act => act.MapFrom(src => src.Name));
        }
    }
}
