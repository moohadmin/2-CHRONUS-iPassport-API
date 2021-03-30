using AutoMapper;
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
        public static void Map(Profile profile)
        {
            profile.CreateMap<UserCreateRequest, UserCreateDto>()                
                .ForMember(des => des.Username, act => act.MapFrom(src => src.Username))
                .ForMember(des => des.Password, act => act.MapFrom(src => src.Password))
                .ForMember(des => des.PasswordIsValid, act => act.MapFrom(src => src.PasswordIsValid))
                .ForMember(des => des.Email, act => act.MapFrom(src => src.Email))
                .ForMember(des => des.Mobile, act => act.MapFrom(src => src.Mobile))
                .ForMember(des => des.Profile, act => act.MapFrom(src => src.Profile))
                .ForMember(des => des.FullName, act => act.MapFrom(src => src.FullName))
                .ForMember(des => des.CPF, act => act.MapFrom(src => src.CPF))
                .ForMember(des => des.RG, act => act.MapFrom(src => src.RG))
                .ForMember(des => des.CNS, act => act.MapFrom(src => src.CNS))
                .ForMember(des => des.Passport, act => act.MapFrom(src => src.Passport))
                .ForMember(des => des.Birthday, act => act.MapFrom(src => src.Birthday))
                .ForMember(des => des.Gender, act => act.MapFrom(src => src.Gender))
                .ForMember(des => des.Breed, act => act.MapFrom(src => src.Breed))
                .ForMember(des => des.BloodType, act => act.MapFrom(src => src.BloodType))
                .ForMember(des => des.Occupation, act => act.MapFrom(src => src.Occupation))
                .ForMember(des => des.Address, act => act.MapFrom(src => src.Address))
                .ForMember(des => des.Photo, act => act.MapFrom(src => src.Photo))
                .ForMember(des => des.InternationalDocument, act => act.MapFrom(src => src.InternationalDocument));

            profile.CreateMap<UserDetails, UserDetailsViewModel>()
                .ReverseMap();

            profile.CreateMap<UserImageRequest, UserImageDto>()
                .ForMember(des => des.ImageFile, act => act.MapFrom(src => src.ImageFile));

            profile.CreateMap<GetRegisteredUsersCountRequest, GetRegisteredUserCountFilter>()
                .ForMember(des => des.Profile, act => act.MapFrom(src => src.ProfileType));

            profile.CreateMap<Users, UserDetailsViewModel>()
                .ForMember(des => des.UserId, act => act.MapFrom(src => src.Id));

            profile.CreateMap<UserAgentCreateRequest, UserAgentCreateDto>()
                .ForMember(des => des.Username, act => act.MapFrom(src => src.Username))
                .ForMember(des => des.Password, act => act.MapFrom(src => src.Password))
                .ForMember(des => des.PasswordIsValid, act => act.MapFrom(src => src.PasswordIsValid))
                .ForMember(des => des.Mobile, act => act.MapFrom(src => src.Mobile))
                .ForMember(des => des.FullName, act => act.MapFrom(src => src.FullName))
                .ForMember(des => des.CPF, act => act.MapFrom(src => src.CPF))
                .ForMember(des => des.Address, act => act.MapFrom(src => src.Address));

            profile.CreateMap<CitizenCreateRequest, CitizenCreateDto>()
                .ForMember(des => des.Test, act => act.MapFrom(src => src.WasTestPerformed.GetValueOrDefault() ? src.Test : null));

            profile.CreateMap<Users, CitizenViewModel>()
                .ForMember(des => des.Telephone, act => act.MapFrom(src => src.PhoneNumber));

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
                .ForMember(des => des.Doses, act => act.MapFrom(src => src.Doses)) //Rever
                .ForMember(des => des.Email, act => act.MapFrom(src => src.Email))
                .ForMember(des => des.GenderId, act => act.MapFrom(src => src.GenderId.HasValue ? src.GenderId : null))
                .ForMember(des => des.HumanRaceId, act => act.MapFrom(src => src.HumanRaceId.HasValue ? src.HumanRaceId : null))
                .ForMember(des => des.Id, act => act.MapFrom(src => src.UserId))
                .ForMember(des => des.NumberOfDoses, act => act.MapFrom(src => src.NumberOfDoses))
                .ForMember(des => des.Occupation, act => act.MapFrom(src => src.Occupation))                
                .ForMember(des => des.PriorityGroupId, act => act.MapFrom(src => src.PriorityGroupId))                
                .ForMember(des => des.Telephone, act => act.MapFrom(src => src.Telephone))
                .ForMember(des => des.WasCovidInfected, act => act.MapFrom(src => src.WasCovidInfected))
                .ForMember(des => des.Test, act => act.MapFrom(src => src.WasTestPerformed.GetValueOrDefault() ? src.Test : null))
                .ForMember(des => des.WasTestPerformed, act => act.MapFrom(src => src.WasTestPerformed));
        }
    }
}
