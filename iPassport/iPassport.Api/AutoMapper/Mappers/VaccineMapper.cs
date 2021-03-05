using AutoMapper;
using iPassport.Application.Models.ViewModels;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;

namespace iPassport.Api.AutoMapper.Mappers
{
    public static class VaccineMapper
    {
        public static void Map(Profile profile)
        {
            profile.CreateMap<VaccineDoseDto, VaccineDoseViewModel>();

            profile.CreateMap<UserVaccineDetailsDto, UserVaccineViewModel>();

            profile.CreateMap<VaccineManufacturer, VaccineManufacturerViewModel>()
                .ReverseMap();

            profile.CreateMap<Disease, DiseaseViewModel>()
                .ReverseMap();

            profile.CreateMap<Vaccine, VaccineViewModel>()
                .ForMember(des => des.ManufacturerName, act => act.MapFrom(src => src.Manufacturer != null ? src.Manufacturer.Name : null));
        }
    }
}
