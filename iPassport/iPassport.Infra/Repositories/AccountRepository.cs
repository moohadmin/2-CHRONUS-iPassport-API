using iPassport.Domain.Entities;
using iPassport.Domain.Repositories;
using iPassport.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Infra.Repositories
{
    public class AccountRepository : Repository<User>, IAccountRepository
    {
        public AccountRepository(iPassportContext context) : base(context) { }

        public async Task<User> BasicLogin(string username, string password) => 
            await _DbSet.Where(x => x.Username == username && x.Password == password).FirstOrDefaultAsync();

        public async Task<User> LoginWithEmail(string email, string password) => 
            await _DbSet.Where(x => x.Email == email && x.Password == password).FirstOrDefaultAsync();
    }
}
