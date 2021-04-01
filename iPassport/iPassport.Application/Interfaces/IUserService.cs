using iPassport.Application.Models;
using iPassport.Application.Models.Pagination;
using iPassport.Domain.Dtos;
using iPassport.Domain.Filters;
using Microsoft.AspNetCore.Http;
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
        Task<PagedResponseApi> GetPaggedCizten(GetCitzenPagedFilter filter);
        Task<ResponseApi> GetCitizenById(Guid id);
        Task<ResponseApi> EditCitizen(CitizenEditDto dto);
        Task ImportUsers(IFormFile file);
        Task<ResponseApi> AddAdmin(AdminCreateDto dto);        
    }
}
