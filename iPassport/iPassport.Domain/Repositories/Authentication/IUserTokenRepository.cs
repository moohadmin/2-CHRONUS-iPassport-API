using iPassport.Domain.Entities.Authentication;
using System;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories.Authentication
{
    public interface IUserTokenRepository : IDisposable
    {
        Task<UserToken> GetByToken(string token);
        Task<UserToken> GetActive(Guid userId);
        Task<bool> Update(UserToken userTkn);
        Task<bool> Add(UserToken userTkn);
    }
}
