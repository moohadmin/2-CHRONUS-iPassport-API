using iPassport.Domain.Entities;
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

        public async Task Update(Users user)
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

        public async Task<int> GetRegisteredUserCount(GetRegisteredUserCountFilter filter) => await _context.Users.Where(x => x.Profile == (int)filter.Profile).CountAsync();

        public async Task<int> GetLoggedCitzenCount() => await _context.Users.Where(u => u.Profile == (int)EProfileType.Citizen && u.LastLogin != null).CountAsync();
        public async Task<int> GetLoggedAgentCount() => await _context.Users.Where(u => u.Profile == (int)EProfileType.Agent && u.LastLogin != null).CountAsync();

        public async Task<PagedData<Users>> FindCitizensByNameParts(GetByNamePartsPagedFilter filter)
        {
            var query = _context.Users.Where(m => m.Profile == (int)EProfileType.Citizen 
                            && (string.IsNullOrWhiteSpace(filter.Initials) || m.FullName.ToLower().Contains(filter.Initials.ToLower())))
            .OrderBy(m => m.FullName);

            return await Paginate(query, filter);
        }

        protected virtual async Task<PagedData<Users>> Paginate(IQueryable<Users> dbSet, PageFilter filter)
        {
            (int take, int skip) = CalcPageOffset(filter);

            var data = await dbSet.Take(take).Skip(skip).ToListAsync();
            var totalPages = data.Count > filter.PageSize ? data.Count / filter.PageSize : 1;

            return new PagedData<Users>() { PageNumber = filter.PageNumber, PageSize = filter.PageSize, TotalPages = totalPages, TotalRecords = data.Count, Data = data };
        }

        protected (int, int) CalcPageOffset(PageFilter filter)
        {
            int skip = (filter.PageNumber - 1) * filter.PageSize;
            int take = skip + filter.PageSize;
            return (take, skip);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
