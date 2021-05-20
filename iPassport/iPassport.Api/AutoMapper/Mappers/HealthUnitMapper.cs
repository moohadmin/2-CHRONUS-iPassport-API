using AutoM = AutoMapper;
using iPassport.Api.Models.Requests.HealthUnit;
using iPassport.Application.Models.ViewModels;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Filters;

namespace iPassport.Api.AutoMapper.Mappers
{
    /// <summary>
    /// Health Unit Mapper Class
    /// </summary>
    public static class HealthUnitMapper
    {
        /// <summary>
        /// Map Method
        /// </summary>
        /// <param name="profile">Auto Mapper Profile Instance</param>
        public static void Map(AutoM.Profile profile)
        {
            profile.CreateMap<HealthUnit, HealthUnitViewModel>()
                 .ForMember(des => des.Id, act => act.MapFrom(src => src.Id))
                 .ForMember(des => des.Name, act => act.MapFrom(src => src.Name))
                 .ForMember(des => des.Cnpj, act => act.MapFrom(src => src.Cnpj))
                 .ForMember(des => des.Email, act => act.MapFrom(src => src.Email))
                 .ForMember(des => des.Responsible.Name, act => act.MapFrom(src => src.ResponsiblePersonName))
                 .ForMember(des => des.Responsible.MobilePhone, act => act.MapFrom(src => src.ResponsiblePersonMobilePhone))
                 .ForMember(des => des.Responsible.Landline, act => act.MapFrom(src => src.ResponsiblePersonLandline))

                 .ForMember(des => des.Responsible.Occupation, act => act.MapFrom(src => src.ResponsiblePersonOccupation))
                 .ForMember(des => des.IsActive, act => act.MapFrom(src => src.Active))
                 .ForMember(des => des.Type, act => act.MapFrom(src => src.Type))
                 .ForMember(des => des.AddressId, act => act.MapFrom(src => src.AddressId));
            
            profile.CreateMap<HealthUnitType, HealthUnitTypeViewModel>()
                .ForMember(des => des.Id, act => act.MapFrom(src => src.Id))
                 .ForMember(des => des.Name, act => act.MapFrom(src => src.Name));

            profile.CreateMap<HealthUnitCreateRequest, HealthUnitCreateDto>()
                .ForMember(des => des.ResponsiblePersonName, act => act.MapFrom(src => src.Responsible.Name))
                .ForMember(des => des.ResponsiblePersonOccupation, act => act.MapFrom(src => src.Responsible.Occupation))
                .ForMember(des => des.ResponsiblePersonLandline, act => act.MapFrom(src => src.Responsible.Landline))
                .ForMember(des => des.ResponsiblePersonMobilePhone, act => act.MapFrom(src => src.Responsible.MobilePhone));

            profile.CreateMap<HealthUnitEditRequest, HealthUnitEditDto>()
                .ForMember(des => des.ResponsiblePersonName, act => act.MapFrom(src => src.Responsible.Name))
                .ForMember(des => des.ResponsiblePersonOccupation, act => act.MapFrom(src => src.Responsible.Occupation))
                .ForMember(des => des.ResponsiblePersonLandline, act => act.MapFrom(src => src.Responsible.Landline))
                .ForMember(des => des.ResponsiblePersonMobilePhone, act => act.MapFrom(src => src.Responsible.MobilePhone));

            profile.CreateMap<HealthUnitTypeDto, HealthUnitTypeViewModel>();

            profile.CreateMap<HealthUnitDto, HealthUnitViewModel>()
                 .ForMember(des => des.IsActive, act => act.MapFrom(src => src.Active));

            profile.CreateMap<GetHealthUnitPagedRequest, GetHealthUnitPagedFilter>();
        }
    }
}
