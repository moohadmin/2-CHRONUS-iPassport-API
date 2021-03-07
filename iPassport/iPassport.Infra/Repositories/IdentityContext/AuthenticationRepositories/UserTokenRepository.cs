using iPassport.Domain.Entities.Authentication;
using iPassport.Domain.Repositories.Authentication;
using iPassport.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace iPassport.Infra.Repositories.AuthenticationRepositories
{
    public class UserTokenRepository : IdentityBaseRepository<UserToken>, IUserTokenRepository
    {
        public UserTokenRepository(PassportIdentityContext context) : base(context)  { }

        public async Task<UserToken> GetByToken(string token) => 
           await _DbSet.FirstOrDefaultAsync(x => x.Value == token);

        public async Task<UserToken> GetActive(Guid userId) =>
            await _DbSet.FirstOrDefaultAsync(x => x.IsActive == true && x.UserId == userId);

    }
}
