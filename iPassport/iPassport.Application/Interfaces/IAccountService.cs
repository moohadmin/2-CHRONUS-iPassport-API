using iPassport.Application.Models;
using iPassport.Domain.Enums;
using System.Threading.Tasks;

namespace iPassport.Application.Interfaces
{
    public interface IAccountService
    {
        Task<ResponseApi> BasicLogin(string username, string password);
        Task<ResponseApi> EmailLogin(string email, string password);
        Task<ResponseApi> SendPin(string phone, EDocumentType doctype, string doc);
        Task<ResponseApi> MobileLogin(int pin, System.Guid userId);
        Task<ResponseApi> ResetPassword(string password, string passwordConfirm);
        Task<ResponseApi> ResendPin(string phone, System.Guid userId);
    }
}