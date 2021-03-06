using iPassport.Domain.Entities.Authentication;
using iPassport.Domain.Enums;
using iPassport.Domain.Filters;
using System;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories.Authentication
{
    public interface IUserRepository
    {
        Task<Users> FindByPhone(string phone);
        Task<Users> FindById(Guid id);
        Task Update(Users user);
        Task<Users> FindByDocument(EDocumentType documentType, string document);
        Task<int> GetLoggedCitzenCount();
        Task<int> GetRegisteredUserCount(GetRegisteredUserCountFilter filter);
        Task<int> GetLoggedAgentCount();
    }
}
