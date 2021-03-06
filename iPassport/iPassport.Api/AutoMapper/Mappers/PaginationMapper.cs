using AutoMapper;
using iPassport.Api.Models.Requests;
using iPassport.Api.Models.Requests.Shared;
using iPassport.Api.Models.Requests.User;
using iPassport.Domain.Filters;

namespace iPassport.Api.AutoMapper.Mappers
{
    public class PaginationMapper
    {
        public static void Map(Profile profile)
        {
            profile.CreateMap<PageFilterRequest, PageFilter>();
            profile.CreateMap<GetPagedVaccinesByManufacuterRequest, GetByIdPagedFilter>()
                    .ForMember(des => des.Id, act => act.MapFrom(src => src.ManufacuterId));

            profile.CreateMap<GetByNamePartsPagedRequest, GetByNamePartsPagedFilter>();

            profile.CreateMap<GetPagedUserVaccinesRequest, GetByIdPagedFilter>()
                    .ForMember(des => des.Id, act => act.MapFrom(src => src.PassportId));

            profile.CreateMap<GetPagedStatesByCountryRequest, GetByIdPagedFilter>()
                    .ForMember(des => des.Id, act => act.MapFrom(src => src.CountryId));
        }
    }
}
