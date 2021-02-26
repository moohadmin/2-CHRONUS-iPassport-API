using iPassport.Domain.Entities;
using iPassport.Domain.Enums;
using System;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories
{
    public interface IUserDetailsRepository : IRepository<UserDetails>
    {
        Task<UserDetails> FindWithUser(Guid id);
        Task<UserDetails> FindByDocument(EDocumentType documentType, string document);
        Task<int> GetLoggedCitzenCount();
    }
}
