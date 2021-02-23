using iPassport.Application.Models;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace iPassport.Application.Interfaces
{
    public interface IUserService
    {
        Task<ResponseApi> Add(UserCreateDto dto);
        Task<ResponseApi> AssociatePlan(Guid planId);
        Task<ResponseApi> GetUserPlan();
        Task<ResponseApi> GetCurrentUser();
        
        /// <summary>
        /// ADD photo to user
        /// </summary>
        /// <param name="userImageDto"></param>
        /// <returns></returns>
        Task<ResponseApi> AddUserImage(UserImageDto userImageDto);
    }
}
