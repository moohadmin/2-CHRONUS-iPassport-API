using iPassport.Application.Models;
using iPassport.Domain.Dtos;
using System.Threading.Tasks;

namespace iPassport.Application.Interfaces
{

    public interface IPassportService
    {
        Task<ResponseApi> Get();
        Task<ResponseApi> AddAccessApproved(PassportUseCreateDto dto);
        Task<ResponseApi> AddAccessDenied(PassportUseCreateDto dto);
    }
}
