using iPassport.Domain.Entities;
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

        public async Task<UserDetails> FindWithUser(Guid id) =>
            await _DbSet.Where(x => x.UserId == id).FirstOrDefaultAsync();

        //public async Task<UserDetails> FindByPhone(string phone) =>
        //    await _DbSet.Where(x => x.)

    }
}
