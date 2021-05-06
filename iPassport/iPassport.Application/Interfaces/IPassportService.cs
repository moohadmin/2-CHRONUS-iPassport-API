using iPassport.Application.Models;
using iPassport.Domain.Dtos;
using System;
using System.Threading.Tasks;

namespace iPassport.Application.Interfaces
{

    public interface IPassportService
    {
        Task<ResponseApi> Get(string imageSize);
        Task<ResponseApi> AddAccessApproved(PassportUseCreateDto dto);
        Task<ResponseApi> AddAccessDenied(PassportUseCreateDto dto);
        Task<ResponseApi> GetPassportUserToValidate(Guid passportDetailsId, string imageSize);
    }
}
