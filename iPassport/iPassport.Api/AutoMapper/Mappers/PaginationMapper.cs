using AutoM = AutoMapper;
using iPassport.Api.Models.Requests;
using iPassport.Api.Models.Requests.Shared;
using iPassport.Api.Models.Requests.User;
using iPassport.Domain.Filters;
using iPassport.Api.Models.Requests.Company;

namespace iPassport.Api.AutoMapper.Mappers
{
    /// <summary>
    /// Pagination Mapper Class
    /// </summary>
    public class PaginationMapper
    {
        /// <summary>
        /// Map Method
        /// </summary>
        /// <param name="profile">Auto Mapper Profile Instance</param>
        public static void Map(AutoM.Profile profile)
        {
            profile.CreateMap<PageFilterRequest, PageFilter>();

            profile.CreateMap<GetPagedVaccinesByManufacuterRequest, GetByIdAndNamePartsPagedFilter>()
                    .ForMember(des => des.Id, act => act.MapFrom(src => src.ManufacuterId));

            profile.CreateMap<GetByNamePartsPagedRequest, GetByNamePartsPagedFilter>();

            profile.CreateMap<GetPagedUserVaccinesByPassportRequest, GetByIdPagedFilter>()
                    .ForMember(des => des.Id, act => act.MapFrom(src => src.PassportId));

            profile.CreateMap<GetPagedStatesByCountryRequest, GetByIdPagedFilter>()
                    .ForMember(des => des.Id, act => act.MapFrom(src => src.CountryId));

            profile.CreateMap<GetPagedCitiesByStateAndNamePartsRequest, GetByIdAndNamePartsPagedFilter>()
                    .ForMember(des => des.Id, act => act.MapFrom(src => src.StateId));

            profile.CreateMap<GetPagedUserVaccinesByPassportRequest, GetByIdPagedFilter>()
                .ForMember(des => des.Id, act => act.MapFrom(src => src.PassportId));

            profile.CreateMap<GetCitzenPagedRequest, GetCitzenPagedFilter>();

            profile.CreateMap<GetAgentPagedRequest, GetAgentPagedFilter>();

            profile.CreateMap<GetAdminUserPagedRequest, GetAdminUserPagedFilter>();

            profile.CreateMap<GetCompaniesPagedRequest, GetCompaniesPagedFilter>();
        }
    }
}
