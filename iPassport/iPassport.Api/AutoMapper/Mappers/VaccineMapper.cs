using AutoM = AutoMapper;
using iPassport.Api.Models.Requests.User;
using iPassport.Application.Models.ViewModels;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;

namespace iPassport.Api.AutoMapper.Mappers
{
    /// <summary>
    /// Vaccine Mapper Class
    /// </summary>
    public static class VaccineMapper
    {
        /// <summary>
        /// Map Method
        /// </summary>
        /// <param name="profile">Auto Mapper Profile Instance</param>
        public static void Map(AutoM.Profile profile)
        {
            profile.CreateMap<VaccineDoseDto, VaccineDoseViewModel>();

            profile.CreateMap<UserVaccineDetailsDto, UserVaccineViewModel>();

            profile.CreateMap<VaccineManufacturer, VaccineManufacturerViewModel>()
                .ReverseMap();

            profile.CreateMap<VaccineManufacturerDto, VaccineManufacturerViewModel>();

            profile.CreateMap<Disease, DiseaseViewModel>()
                .ReverseMap();

            profile.CreateMap<Vaccine, VaccineViewModel>()
                .ForMember(des => des.ManufacturerName, act => act.MapFrom(src => src.Manufacturer != null ? src.Manufacturer.Name : null));

            profile.CreateMap<UserVaccineCreateRequest, UserVaccineCreateDto>()
                .ForMember(des => des.VaccineId, act => act.MapFrom(src => src.Vaccine));

           profile.CreateMap<UserVaccineEditRequest, UserVaccineEditDto>()
                .ForMember(des => des.VaccineId, act => act.MapFrom(src => src.Vaccine))
                .ForMember(des => des.Id, act => act.MapFrom(src => src.Id));
        }
    }
}
