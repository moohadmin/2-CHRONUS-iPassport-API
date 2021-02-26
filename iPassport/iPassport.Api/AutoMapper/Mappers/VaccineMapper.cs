using AutoMapper;
using iPassport.Api.Models.Requests;
using iPassport.Application.Models.ViewModels;
using iPassport.Domain.Entities;
using iPassport.Domain.Filters;

namespace iPassport.Api.AutoMapper.Mappers
{
    public static class VaccineMapper
    {
        public static void Map(Profile profile)
        {
            profile.CreateMap<UserVaccine, UserVaccineViewModel>()
                .ForMember(des => des.Manufacturer, act => act.MapFrom(src => src.Vaccine != null && src.Vaccine.Manufacturer != null ? src.Vaccine.Manufacturer.Name : null))
                .ReverseMap();

            profile.CreateMap<VaccineManufacturer, VaccineManufacturerViewModel>()
                .ReverseMap();

            profile.CreateMap<Disease, DiseaseViewModel>()
                .ReverseMap();
        }
    }
}
