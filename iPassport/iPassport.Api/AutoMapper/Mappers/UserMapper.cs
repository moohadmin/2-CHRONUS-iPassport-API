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
    public static class UserMapper
    {
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

            profile.CreateMap<CitizenCreateRequest, CitizenCreateDto>();
                

            profile.CreateMap<Users, CitizenByNameViewModel>()
                .ForMember(des => des.Id, act => act.MapFrom(src => src.Id))
                .ForMember(des => des.FullName, act => act.MapFrom(src => src.FullName))
                .ForMember(des => des.CNS, act => act.MapFrom(src => src.CNS))
                .ForMember(des => des.CPF, act => act.MapFrom(src => src.CPF));
        }
    }
}
