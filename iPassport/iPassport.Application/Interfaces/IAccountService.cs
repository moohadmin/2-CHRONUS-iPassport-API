using System.Threading.Tasks;
using iPassport.Application.Models;
using iPassport.Domain.Enums;

namespace iPassport.Application.Interfaces
{
    public interface IAccountService
    {
        Task<ResponseApi> BasicLogin(string username, string password);
        Task<ResponseApi> EmailLogin(string email, string password);
        Task<ResponseApi> SendPin(string phone, EDocumentType doctype, string doc);
        Task<ResponseApi> MobileLogin(int pin, System.Guid userId);
    }
}