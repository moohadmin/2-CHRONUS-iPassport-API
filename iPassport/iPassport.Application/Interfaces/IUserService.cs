using iPassport.Application.Models;
using iPassport.Domain.Dtos;
using iPassport.Domain.Filters;
using System;
using System.Threading.Tasks;

namespace iPassport.Application.Interfaces
{
    public interface IUserService
    {
        Task<ResponseApi> AddCitizen(CitizenCreateDto dto);
        Task<ResponseApi> AssociatePlan(Guid planId);
        Task<ResponseApi> GetUserPlan();
        Task<ResponseApi> GetCurrentUser();
        Task<ResponseApi> AddUserImage(UserImageDto userImageDto);
        Task<ResponseApi> GetLoggedCitzenCount();
        Task<ResponseApi> GetRegisteredUserCount(GetRegisteredUserCountFilter filter);
        Task<ResponseApi> GetLoggedAgentCount();

        Task<ResponseApi> AddAgent(UserAgentCreateDto dto);
    }
}
