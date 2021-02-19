using iPassport.Application.Models;
using iPassport.Domain.Dtos;
using System.Threading.Tasks;

namespace iPassport.Application.Interfaces
{
    public interface IUserService
    {
        Task<ResponseApi> Add(UserCreateDto dto);
        
        /// <summary>
        /// ADD photo to user
        /// </summary>
        /// <param name="userImageDto"></param>
        /// <returns></returns>
        Task<ResponseApi> AddUserImage(UserImageDto userImageDto);
    }
}
