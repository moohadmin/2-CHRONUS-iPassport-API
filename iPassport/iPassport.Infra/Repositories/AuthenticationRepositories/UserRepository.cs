using iPassport.Domain.Entities.Authentication;
using iPassport.Domain.Repositories.Authentication;
using iPassport.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Infra.Repositories.AuthenticationRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PassportIdentityContext _context;

        public UserRepository(PassportIdentityContext context) => _context = context;


        public async Task<Users> FindByPhone(string phone) =>
            await _context.Users.Where(x => x.PhoneNumber == phone).FirstOrDefaultAsync();

        public async Task<Users> FindById(Guid id) =>
            await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
    }

}
