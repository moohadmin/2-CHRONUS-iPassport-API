using AutoMapper;
using iPassport.Application.Models.ViewModels;
using iPassport.Domain.Entities;

namespace iPassport.Api.AutoMapper.Mappers
{
    public static class VaccineMapper
    {
        public static void Map(Profile profile)
        {
            profile.CreateMap<Vaccine, VaccineViewModel>()
                .ReverseMap();
        }
    }
}
