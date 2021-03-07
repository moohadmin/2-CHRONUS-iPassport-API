using iPassport.Domain.Entities.Authentication;
using System;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories.Authentication
{
    public interface IUserTokenRepository : IIdentityBaseRepository<UserToken>
    {
        Task<UserToken> GetByToken(string token);
        Task<UserToken> GetActive(Guid userId);
    }
}
