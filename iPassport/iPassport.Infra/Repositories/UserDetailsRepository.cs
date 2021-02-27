using iPassport.Domain.Entities;
using iPassport.Domain.Enums;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories;
using iPassport.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Infra.Repositories
{
    public class UserDetailsRepository : Repository<UserDetails>, IUserDetailsRepository
    {
        public UserDetailsRepository(iPassportContext context) : base(context) { }

        public async Task<UserDetails> GetByUserId(Guid id) =>
            await _DbSet.Include(u => u.Plan).Where(x => x.UserId == id).FirstOrDefaultAsync();

        public async Task<int> GetRegisteredUserCount(GetRegisteredUserCountFilter filter) => await _DbSet.Where(x => (int)filter.Profile == 0 || x.Profile == (int)filter.Profile).CountAsync();

        public async Task<int> GetLoggedCitzenCount() => await _DbSet.Where(u => u.Profile == (int)EProfileType.Citizen).CountAsync();
    }
}
