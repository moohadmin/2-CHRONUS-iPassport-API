using AutoMapper;
using iPassport.Api.Models.Requests;
using iPassport.Application.Models.ViewModels;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;

namespace iPassport.Api.AutoMapper.Mappers
{
    public static class CountryMapper
    {
        public static void Map(Profile profile)
        {
            profile.CreateMap<Country, CountryViewModel>();

            profile.CreateMap<CountryCreateRequest, CountryCreateDto>();

        }
    }
}
