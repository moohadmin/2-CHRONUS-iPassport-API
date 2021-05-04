using AutoM = AutoMapper;
using iPassport.Api.Models.Requests;
using iPassport.Api.Models.Requests.User;
using iPassport.Application.Models.ViewModels;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Entities.Authentication;
using iPassport.Domain.Filters;

namespace iPassport.Api.AutoMapper.Mappers
{
    /// <summary>
    /// User Mapper Class
    /// </summary>
    public static class UserMapper
    {
        /// <summary>
        /// Map Method
        /// </summary>
        /// <param name="profile">Auto Mapper Profile Instance</param>
        public static void Map(AutoM.Profile profile)
        {
            profile.CreateMap<UserDetails, UserDetailsViewModel>()
                .ReverseMap();

            profile.CreateMap<UserImageRequest, UserImageDto>()
                .ForMember(des => des.ImageFile, act => act.MapFrom(src => src.ImageFile));
            
            profile.CreateMap<GetRegisteredUsersCountRequest, GetRegisteredUserCountFilter>()
                .ForMember(des => des.UserType, act => act.MapFrom(src => src.ProfileType));

            profile.CreateMap<Users, UserDetailsViewModel>()
                .ForMember(des => des.UserId, act => act.MapFrom(src => src.Id));

            profile.CreateMap<UserAgentCreateRequest, UserAgentDto>()
                .ForMember(des => des.Password, act => act.MapFrom(src => src.Password))
                .ForMember(des => des.Mobile, act => act.MapFrom(src => src.CellphoneNumber))
                .ForMember(des => des.FullName, act => act.MapFrom(src => src.CompleteName))
                .ForMember(des => des.CPF, act => act.MapFrom(src => src.Cpf))
                .ForMember(des => des.Address, act => act.MapFrom(src => src.Address));

            profile.CreateMap<CitizenCreateRequest, CitizenCreateDto>();

            profile.CreateMap<Users, CitizenViewModel>()
                .ForMember(des => des.Telephone, act => act.MapFrom(src => src.PhoneNumber));

            profile.CreateMap<Users, AgentViewModel>()
                .ForMember(des => des.CompanyName, act => act.MapFrom(src => src.Company.Name))
                .ForMember(des => des.IsActive, act => act.MapFrom(src => src.IsActive(Domain.Enums.EUserType.Agent)));


            profile.CreateMap<CitizenDetailsDto, CitizenDetailsViewModel>();

            profile.CreateMap<CitizenEditRequest, CitizenEditDto>()
                .ForMember(des => des.CompleteName, act => act.MapFrom(src => src.CompleteName))
                .ForMember(des => des.Address, act => act.MapFrom(src => src.Address))
                .ForMember(des => des.Birthday, act => act.MapFrom(src => src.Birthday))
                .ForMember(des => des.BloodTypeId, act => act.MapFrom(src => src.BloodTypeId.HasValue ? src.BloodTypeId : null))
                .ForMember(des => des.Bond, act => act.MapFrom(src => src.Bond))
                .ForMember(des => des.Cns, act => act.MapFrom(src => src.Cns))
                .ForMember(des => des.CompanyId, act => act.MapFrom(src => src.CompanyId.HasValue ? src.CompanyId : null))
                .ForMember(des => des.Cpf, act => act.MapFrom(src => src.Cpf))
                .ForMember(des => des.Doses, act => act.MapFrom(src => src.Doses)) 
                .ForMember(des => des.Email, act => act.MapFrom(src => src.Email))
                .ForMember(des => des.GenderId, act => act.MapFrom(src => src.GenderId.HasValue ? src.GenderId : null))
                .ForMember(des => des.HumanRaceId, act => act.MapFrom(src => src.HumanRaceId.HasValue ? src.HumanRaceId : null))
                .ForMember(des => des.Id, act => act.MapFrom(src => src.UserId))
                .ForMember(des => des.NumberOfDoses, act => act.MapFrom(src => src.NumberOfDoses))
                .ForMember(des => des.Occupation, act => act.MapFrom(src => src.Occupation))                
                .ForMember(des => des.PriorityGroupId, act => act.MapFrom(src => src.PriorityGroupId))                
                .ForMember(des => des.Telephone, act => act.MapFrom(src => src.Telephone))
                .ForMember(des => des.WasCovidInfected, act => act.MapFrom(src => src.WasCovidInfected))
                .ForMember(des => des.Test, act => act.MapFrom(src => src.Test))
                .ForMember(des => des.WasTestPerformed, act => act.MapFrom(src => src.WasTestPerformed));

            profile.CreateMap<AdminCreateRequest, AdminDto>();

            profile.CreateMap<AdminDetailsDto, AdminDetailsViewModel>();

            profile.CreateMap<Users, AdminUserViewModel>()
                .ForMember(des => des.CompanyName, act => act.MapFrom(src => src.Company != null ? src.Company.Name : null))
                .ForMember(des => des.ProfileName, act => act.MapFrom(src => src.Profile != null ? src.Profile.Name : null))
                .ForMember(des => des.IsActive, act => act.MapFrom(src => src.IsActive(Domain.Enums.EUserType.Admin)))
                .ForMember(des => des.CompleteName, act => act.MapFrom(src => src.FullName));

            profile.CreateMap<AdminEditRequest, AdminDto>()
                .ForMember(des => des.Id, act => act.MapFrom(src => src.UserId));

            profile.CreateMap<Users, UserViewModel>();
        }
    }
}
