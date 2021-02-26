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

        public async Task<UserDetails> FindWithUser(Guid id) =>
            await _DbSet.Where(x => x.UserId == id).FirstOrDefaultAsync();

        public async Task<UserDetails> FindByDocument(EDocumentType documentType, string document)
        {
            return documentType switch
            {
                EDocumentType.CPF => await _DbSet.Where(x => x.CPF == document).FirstOrDefaultAsync(),
                EDocumentType.RG => await _DbSet.Where(x => x.RG == document).FirstOrDefaultAsync(),
                EDocumentType.Passport => await _DbSet.Where(x => x.PassportDoc == document).FirstOrDefaultAsync(),
                EDocumentType.CNS => await _DbSet.Where(x => x.CNS == document).FirstOrDefaultAsync(),
                EDocumentType.InternationalDocument => await _DbSet.Where(x => x.InternationalDocument == document).FirstOrDefaultAsync(),
                _ => null,
            };
        }

        public async Task<int> GetLoggedCitzenCount() => await _DbSet.Where(u => u.Profile == (int)EProfileType.Citizen).CountAsync();
    }
}
