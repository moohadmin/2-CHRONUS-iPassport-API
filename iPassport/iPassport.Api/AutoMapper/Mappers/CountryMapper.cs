using AutoMapper;
using iPassport.Application.Models.ViewModels;
using iPassport.Domain.Entities;

namespace iPassport.Api.AutoMapper.Mappers
{
    public static class CountryMapper
    {
        public static void Map(Profile profile)
        {
            profile.CreateMap<Country, CountryViewModel>();
            
        }
    }
}
