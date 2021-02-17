using iPassport.Domain.Entities;
using iPassport.Domain.Repositories;
using iPassport.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Infra.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(iPassportContext context) : base(context) { }

        public async Task<User> FindWithDoc(string doc) => 
            await _DbSet.Where(x => x.UserDetails.CPF == doc || x.UserDetails.CNS == doc).FirstOrDefaultAsync();
    }
}
