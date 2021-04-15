using AutoM = AutoMapper;
using iPassport.Api.Models.Requests;
using iPassport.Application.Models.ViewModels;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Api.Models.Requests.Company;
using iPassport.Domain.Filters;

namespace iPassport.Api.AutoMapper.Mappers
{
    /// <summary>
    /// Company Mapper Class
    /// </summary>
    public static class CompanyMapper
    {
        /// <summary>
        /// Map Method
        /// </summary>
        /// <param name="profile">Auto Mapper Profile Instance</param>
        public static void Map(AutoM.Profile profile)
        {
            profile.CreateMap<Company, CompanyViewModel>()
                .ForMember(des => des.Active, act => act.MapFrom(src => src.IsActive()));

            profile.CreateMap<CompanyCreateRequest, CompanyCreateDto>()
                .ForMember(des => des.Name, act => act.MapFrom(src => src.Name))
                .ForMember(des => des.TradeName, act => act.MapFrom(src => src.TradeName))
                .ForMember(des => des.Cnpj, act => act.MapFrom(src => src.Cnpj))
                .ForMember(des => des.Address, act => act.MapFrom(src => src.Address))
                .ForMember(des => des.SegmentId, act => act.MapFrom(src => src.SegmentId))
                .ForMember(des => des.IsHeadquarters, act => act.MapFrom(src => src.IsHeadquarters))
                .ForMember(des => des.Responsible, act => act.MapFrom(src => src.Responsible))                
                .ForMember(des => des.IsActive, act => act.MapFrom(src => src.IsActive));

            profile.CreateMap<CompanyEditRequest, CompanyEditDto>();

            profile.CreateMap<CompanyDto, CompanyViewModel>();

            profile.CreateMap<CompanyType, CompanyTypeViewModel>();

            profile.CreateMap<CompanySegment, CompanySegmentViewModel>();

            profile.CreateMap<Company, HeadquarterCompanyViewModel>();

            profile.CreateMap<GetHeadquarterCompanyRequest, GetHeadquarterCompanyFilter>();

            profile.CreateMap<CompanyResponsibleCreateRequest, CompanyResponsibleDto>();
            
            profile.CreateMap<CompanyResponsibleEditRequest, CompanyResponsibleDto>();

            profile.CreateMap<Company, CompanyCreateResponseViewModel>()
                .ForMember(des => des.Id, act => act.MapFrom(src => src.Id));

            profile.CreateMap<CompanyResponsible, CompanyResponsibleViewModel>();

            profile.CreateMap<Company, CompanySubsidiaryCandidateResponseViewModel>()
                .ForMember(des => des.Id, act => act.MapFrom(src => src.Id))
                .ForMember(des => des.CompanyName, act => act.MapFrom(src => src.Name))
                .ForMember(des => des.SegmentName, act => act.MapFrom(src => src.Segment == null ? null : src.Segment.Name));

            profile.CreateMap<Company, CompanySubsidiaryCandidateViewModel>()
                .ForMember(des => des.Id, act => act.MapFrom(src => src.Id))
                .ForMember(des => des.Name, act => act.MapFrom(src => src.Name))
                .ForMember(des => des.Cnpj, act => act.MapFrom(src => src.Cnpj))
                .ForMember(des => des.Segment, act => act.MapFrom(src => src.Segment == null ? null : src.Segment.Name));

            profile.CreateMap<CompanySegmentDto, CompanySegmentViewModel>();
            profile.CreateMap<CompanyAssociatedDto, CompanyViewModel>();
        }
    }
}
