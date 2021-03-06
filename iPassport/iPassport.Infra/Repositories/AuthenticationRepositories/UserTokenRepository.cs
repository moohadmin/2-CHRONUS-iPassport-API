using iPassport.Domain.Entities.Authentication;
using iPassport.Domain.Repositories.Authentication;
using iPassport.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace iPassport.Infra.Repositories.AuthenticationRepositories
{
    public class UserTokenRepository : IUserTokenRepository
    {
        private readonly PassportIdentityContext _context;
        private readonly DbSet<UserToken> _dDset;
        
        public UserTokenRepository(PassportIdentityContext context)
        {
            _context = context;
            _dDset = context.Set<UserToken>();
        }

        public async Task<UserToken> GetByToken(string token) => 
           await _dDset.FirstOrDefaultAsync(x => x.Value == token);

        public async Task<UserToken> GetActive(Guid userId) =>
            await _dDset.FirstOrDefaultAsync(x => x.IsActive == true && x.UserId == userId);

        public async Task<bool> Add(UserToken userTkn)
        {
            _dDset.Add(userTkn);
            
            var res = await _context.SaveChangesAsync();
            
            return res > 0;
        }

        public async Task<bool> Update(UserToken userTkn)
        {
            _dDset.Update(userTkn);

            var res = await _context.SaveChangesAsync();

            return res > 0;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
