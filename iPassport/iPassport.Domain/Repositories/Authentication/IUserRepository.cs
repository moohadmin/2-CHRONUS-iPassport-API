using iPassport.Domain.Entities.Authentication;
using iPassport.Domain.Enums;
using System;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories.Authentication
{
    public interface IUserRepository
    {
        Task<Users> FindByPhone(string phone);
        Task<Users> FindById(Guid id);
        void Update(Users user);
        Task<Users> FindByDocument(EDocumentType documentType, string document);
    }
}
