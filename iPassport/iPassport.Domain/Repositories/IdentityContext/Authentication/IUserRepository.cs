using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Entities.Authentication;
using iPassport.Domain.Enums;
using iPassport.Domain.Filters;
using System;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories.Authentication
{
    public interface IUserRepository
    {
        Task<Users> GetByPhone(string phone);
        Task<Users> GetById(Guid id);
        Task<Users> GetLoadedCitizenById(Guid id);
        Task Update(Users user);
        Task<Users> GetByDocument(EDocumentType documentType, string document);
        Task<int> GetLoggedCitzenCount();
        Task<int> GetRegisteredUserCount(GetRegisteredUserCountFilter filter);
        Task<int> GetLoggedAgentCount();
        Task<PagedData<Users>> GetPagedAgent(GetAgentPagedFilter filter, AccessControlDTO dto);
        Task<PagedData<Users>> GetPaggedCizten(GetCitzenPagedFilter filter, AccessControlDTO dto);
        Task<Users> GetByEmail(string email);
        Task<Users> GetAdminById(Guid id);
        Task<PagedData<Users>> GetPagedAdmins(GetAdminUserPagedFilter filter, AccessControlDTO dto);
        Task<Users> GetByUsername(string username);
    }
}
