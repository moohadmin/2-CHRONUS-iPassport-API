using iPassport.Domain.Entities.Authentication;
using iPassport.Domain.Enums;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories.Authentication;
using iPassport.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Infra.Repositories.AuthenticationRepositories
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private readonly PassportIdentityContext _context;

        public UserRepository(PassportIdentityContext context) => _context = context;

        public async Task<Users> FindByPhone(string phone) =>
            await _context.Users.Where(x => x.PhoneNumber == phone).FirstOrDefaultAsync();

        public async Task<Users> FindById(Guid id) =>
            await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();

        public async void Update(Users user)
        {
            user.SetUpdateDate();

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Users> FindByDocument(EDocumentType documentType, string document)
        {
            return documentType switch
            {
                EDocumentType.CPF => await _context.Users.Where(x => x.CPF == document).FirstOrDefaultAsync(),
                EDocumentType.RG => await _context.Users.Where(x => x.RG == document).FirstOrDefaultAsync(),
                EDocumentType.Passport => await _context.Users.Where(x => x.PassportDoc == document).FirstOrDefaultAsync(),
                EDocumentType.CNS => await _context.Users.Where(x => x.CNS == document).FirstOrDefaultAsync(),
                EDocumentType.InternationalDocument => await _context.Users.Where(x => x.InternationalDocument == document).FirstOrDefaultAsync(),
                _ => null,
            };
        }

        public async Task<int> GetRegisteredUserCount(GetRegisteredUserCountFilter filter) => await _context.Users.Where(x => (int)filter.Profile == 0 || x.Profile == (int)filter.Profile).CountAsync();

        public async Task<int> GetLoggedCitzenCount() => await _context.Users.Where(u => u.Profile == (int)EProfileType.Citizen && u.LastLogin != null).CountAsync();
        public async Task<int> GetLoggedAgentCount() => await _context.Users.Where(u => u.Profile == (int)EProfileType.Agent && u.LastLogin != null).CountAsync();

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
