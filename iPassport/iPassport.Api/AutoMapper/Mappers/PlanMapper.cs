using AutoMapper;
using iPassport.Api.Models.Requests;
using iPassport.Application.Models.ViewModels;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;

namespace iPassport.Api.AutoMapper.Mappers
{
    public class PlanMapper
    {
        public static void Map(Profile profile)
        {
            profile.CreateMap<Plan, PlanViewModel>()
                .ReverseMap();

            profile.CreateMap<PlanCreateRequest, PlanCreateDto>();
        }
    }
}
