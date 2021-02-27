using iPassport.Domain.Entities;
using iPassport.Domain.Enums;
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

        public async Task<int> GetLoggedCitzenCount() => await _DbSet.Where(u => u.Profile == (int)EProfileType.Citizen).CountAsync();
    }
}
