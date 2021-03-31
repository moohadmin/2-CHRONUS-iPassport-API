using iPassport.Application.Exceptions;
using iPassport.Domain.Entities;
using iPassport.Domain.Entities.Authentication;
using iPassport.Domain.Enums;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories.Authentication;
using iPassport.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Infra.Repositories.AuthenticationRepositories
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private readonly PassportIdentityContext _context;

        public UserRepository(PassportIdentityContext context) => _context = context;

        public async Task<Users> GetByPhone(string phone) =>
            await _context.Users.Where(x => x.PhoneNumber == phone).FirstOrDefaultAsync();

        public async Task<Users> GetById(Guid id) =>
            await _context.Users.Where(x => x.Id == id).Include(y => y.Address).FirstOrDefaultAsync();

        public async Task<Users> GetLoadedUsersById(Guid id) =>
            await _context.Users
                .Include(x => x.Address).ThenInclude(x => x.City).ThenInclude(x => x.State).ThenInclude(x => x.Country)
                .Include(x => x.Company)
                .Include(x => x.BBloodType)
                .Include(x => x.GGender)
                .Include(x => x.HumanRace)
                .Where(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<Users> GetByEmail(string email) =>
           await _context.Users
               .Include(x => x.Profile)               
               .Where(x => x.NormalizedEmail == email.ToUpper()).FirstOrDefaultAsync();

        public async Task Update(Users user)
        {
            try
            {
                user.SetUpdateDate();

                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.GetType() == (typeof(PostgresException)))
                {
                    var key = ((PostgresException)ex.InnerException).ConstraintName.Split('_').Last();

                    throw new UniqueKeyException(key, ex);
                }

                throw new PersistenceException(ex);
            }
            catch (Exception ex)
            {
                throw new PersistenceException(ex);
            }
        }

        public async Task<Users> GetByDocument(EDocumentType documentType, string document)
        {
            IQueryable<Users> query = _context.Users;

            return await GetUserDocument(query, documentType, document).FirstOrDefaultAsync();
        }

        public async Task<int> GetRegisteredUserCount(GetRegisteredUserCountFilter filter) => await _context.Users.Where(x => x.UserType == (int)filter.UserType).CountAsync();

        public async Task<int> GetLoggedCitzenCount() => await _context.Users.Where(u => u.UserType == (int)EProfileType.Citizen && u.LastLogin != null).CountAsync();
        public async Task<int> GetLoggedAgentCount() => await _context.Users.Where(u => u.UserType == (int)EProfileType.Agent && u.LastLogin != null).CountAsync();

        public async Task<PagedData<Users>> GetPaggedCizten(GetCitzenPagedFilter filter)
        {
            IQueryable<Users> query = _context.Users;

            if (filter.DocumentType.HasValue)
                query = GetUserDocument(query, filter.DocumentType.Value, filter.Document);

            if (filter.CompanyId.HasValue)
                query = query.Where(x => x.CompanyId == filter.CompanyId);

            if (filter.CityId.HasValue)
                query = query.Where(x => x.Address.CityId == filter.CityId);

            if (filter.StateId.HasValue)
                query = query.Where(x => x.Address.City.StateId == filter.StateId);

            if (filter.CountryId.HasValue)
                query = query.Where(x => x.Address.City.State.CountryId == filter.CountryId);

            query = query.Where(m => m.UserType == (int)EProfileType.Citizen
                              && (string.IsNullOrWhiteSpace(filter.Initials) || m.FullName.ToLower().Contains(filter.Initials.ToLower()))
                              && (string.IsNullOrWhiteSpace(filter.Telephone) || m.PhoneNumber.ToLower().StartsWith(filter.Telephone.ToLower())))
                      .OrderBy(m => m.FullName);

            return await Paginate(query, filter);
        }

        protected virtual async Task<PagedData<Users>> Paginate(IQueryable<Users> dbSet, PageFilter filter)
        {
            (int take, int skip) = CalcPageOffset(filter);

            var dataCount = await dbSet.CountAsync();
            var data = await dbSet.Take(take).Skip(skip).ToListAsync();

            int totalPages = 0;
            if (dataCount < filter.PageSize)
            {
                totalPages = 1;
            }
            else
            {
                totalPages = dataCount / filter.PageSize;
                totalPages = dataCount % filter.PageSize > 0 ? totalPages + 1 : totalPages;
            }

            return new PagedData<Users>() { PageNumber = filter.PageNumber, PageSize = filter.PageSize, TotalPages = totalPages, TotalRecords = dataCount, Data = data };
        }

        protected (int, int) CalcPageOffset(PageFilter filter)
        {
            int skip = (filter.PageNumber - 1) * filter.PageSize;
            int take = skip + filter.PageSize;
            return (take, skip);
        }

        private IQueryable<Users> GetUserDocument(IQueryable<Users> query, EDocumentType documentType, string document)
        {
            return documentType switch
            {
                EDocumentType.CPF => query.Where(x => x.CPF == document),
                EDocumentType.RG => query.Where(x => x.RG == document),
                EDocumentType.Passport => query.Where(x => x.PassportDoc == document),
                EDocumentType.CNS => query.Where(x => x.CNS == document),
                EDocumentType.InternationalDocument => query.Where(x => x.InternationalDocument == document),
                _ => null,
            };
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
