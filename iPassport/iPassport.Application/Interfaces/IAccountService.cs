using System.Threading.Tasks;
using iPassport.Application.Models;

namespace iPassport.Application.Interfaces
{
    public interface IAccountService
    {
        Task<ResponseApi> BasicLogin(string username, string password);
        Task<ResponseApi> EmailLogin(string email, string password);
        ResponseApi SendPin(string phone, string doctype, string doc);
    }
}