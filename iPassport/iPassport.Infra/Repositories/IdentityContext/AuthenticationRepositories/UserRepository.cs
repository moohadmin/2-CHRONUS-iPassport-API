using iPassport.Application.Exceptions;
using iPassport.Domain.Dtos;
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

        private IQueryable<Users> CitizenAccessControllBaseQuery(AccessControlDTO accessControl)
        {
            var query = _context.Users.AsQueryable();

            if (accessControl.Profile == EProfileKey.government.ToString())
            {
                if (accessControl.CityId.HasValue && accessControl.CityId.Value != Guid.Empty)
                    query = query.Where(x => x.Address.CityId == accessControl.CityId.Value);

                if (accessControl.StateId.HasValue && accessControl.StateId.Value != Guid.Empty)
                    query = query.Where(x => x.Address.City.StateId == accessControl.StateId.Value);

                if (accessControl.CountryId.HasValue && accessControl.CountryId.Value != Guid.Empty)
                    query = query.Where(x => x.Address.City.State.CountryId == accessControl.CountryId.Value);
            }
            else if (accessControl.Profile == EProfileKey.business.ToString() && accessControl.CompanyId.HasValue && accessControl.CompanyId.Value != Guid.Empty)
                query = query.Where(x => x.CompanyId == accessControl.CompanyId.Value);
            else if (accessControl.Profile == EProfileKey.healthUnit.ToString())
            {
                query = query.Where(x => (accessControl.CityId == null || x.Address.CityId == accessControl.CityId.Value)
                    || accessControl.FilterIds.Contains(x.Id));
            }

            return query;
        }

        private IQueryable<Users> AdminAccessControllBaseQuery(AccessControlDTO accessControl)
        {
            var query = _context.Users
                .Include(x => x.Company)
                .ThenInclude(x => x.Segment)
                .AsQueryable();

            if (accessControl.Profile == EProfileKey.government.ToString())
            {
                if (accessControl.CityId.HasValue && accessControl.CityId.Value != Guid.Empty)
                    query = query.Where(x => x.Company.Address.CityId == accessControl.CityId.Value && x.Company.Segment.Identifyer == (int)ECompanySegmentType.Municipal);

                if (accessControl.StateId.HasValue && accessControl.StateId.Value != Guid.Empty)
                    query = query.Where(x => x.Company.Address.City.StateId == accessControl.StateId.Value && x.Company.Segment.Identifyer == (int)ECompanySegmentType.State);

                if (accessControl.CountryId.HasValue && accessControl.CountryId.Value != Guid.Empty)
                    query = query.Where(x => x.Company.Address.City.State.CountryId == accessControl.CountryId.Value && x.Company.Segment.Identifyer == (int)ECompanySegmentType.Federal);
            }
            return query;
        }

        public async Task<Users> GetByPhone(string phone) =>
            await _context.Users.Where(x => x.PhoneNumber == phone).FirstOrDefaultAsync();

        public async Task<Users> GetById(Guid id) =>
            await _context.Users.Where(x => x.Id == id).Include(y => y.Address)
            .Include(x => x.UserUserTypes).ThenInclude(x => x.UserType)
            .FirstOrDefaultAsync();

        public async Task<Users> GetAdminById(Guid id) =>
            await _context.Users
                .Include(x => x.Profile)
                .Include(x => x.Company)
                .Include(x => x.UserUserTypes).ThenInclude(x => x.UserType)
                .FirstOrDefaultAsync(x => x.Id == id && x.UserUserTypes.Any(x => x.UserType.Identifyer == (int)EUserType.Admin));
                

        public async Task<Users> GetLoadedCitizenById(Guid id) =>
            await _context.Users
                .Include(x => x.Address).ThenInclude(x => x.City).ThenInclude(x => x.State).ThenInclude(x => x.Country)
                .Include(x => x.Company)
                .Include(x => x.BloodType)
                .Include(x => x.Gender)
                .Include(x => x.HumanRace)
                .Include(x => x.UserUserTypes).ThenInclude(y => y.UserType)
                .Where(x => x.Id == id && x.UserUserTypes.Any(y => y.UserType.Identifyer == (int)EUserType.Citizen)).FirstOrDefaultAsync();

        public async Task<Users> GetByEmail(string email) =>
           await _context.Users
                .Include(x => x.Profile)
                .Include(x => x.Company).ThenInclude(x => x.Address).ThenInclude(x => x.City).ThenInclude(x => x.State)
                .Include(x => x.Company).ThenInclude(x => x.Segment).ThenInclude(x => x.CompanyType)
                .Include(x => x.UserUserTypes).ThenInclude(x => x.UserType)
               .Where(x => x.NormalizedEmail == email.ToUpper()).FirstOrDefaultAsync();

        public async Task<Users> GetByUsername(string username)
            =>
            await _context.Users                
                .Include(x => x.UserUserTypes).ThenInclude(x => x.UserType)
               .Where(x => x.NormalizedUserName == username.ToUpper()).FirstOrDefaultAsync();

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

        public async Task<int> GetRegisteredUserCount(GetRegisteredUserCountFilter filter) => await _context.Users.Where(x => x.UserUserTypes.Any(y => y.UserType.Identifyer == filter.UserType)).CountAsync();

        public async Task<int> GetLoggedCitzenCount() => await _context.Users.Where(u => u.UserUserTypes.Any(y => y.UserType.Identifyer == (int)EUserType.Citizen && y.LastLogin != null)).CountAsync();
        public async Task<int> GetLoggedAgentCount() => await _context.Users.Where(u => u.UserUserTypes.Any(y => y.UserType.Identifyer == (int)EUserType.Agent && y.LastLogin != null )).CountAsync();

        public async Task<PagedData<Users>> GetPaggedCizten(GetCitzenPagedFilter filter, AccessControlDTO dto)
        {
            IQueryable<Users> query = CitizenAccessControllBaseQuery(dto);

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

            query = query.Where(m => m.UserUserTypes.Any(y => y.UserType.Identifyer == (int)EUserType.Citizen)
                              && (string.IsNullOrWhiteSpace(filter.Initials) || m.FullName.ToLower().Contains(filter.Initials.ToLower()))
                              && (string.IsNullOrWhiteSpace(filter.Telephone) || m.PhoneNumber.ToLower().Contains(filter.Telephone.ToLower())))
                      .OrderBy(m => m.FullName);

            return await Paginate(query, filter);
        }

        public async Task<PagedData<Users>> GetPagedAdmins(GetAdminUserPagedFilter filter, AccessControlDTO dto)
        {
            IQueryable<Users> query = AdminAccessControllBaseQuery(dto);

            query = query
                .Include(x => x.Company)
                .Include(x => x.Profile)
                .Include(x => x.UserUserTypes).ThenInclude(x => x.UserType)
                .Where(x => x.UserUserTypes.Any(y => y.UserType.Identifyer == (int)EUserType.Admin)
                    && (filter.CompanyId == null || filter.CompanyId == x.CompanyId)
                    && (filter.ProfileId == null || filter.ProfileId == x.ProfileId)
                    && (filter.Cpf == null || filter.Cpf == x.CPF)
                    && (filter.Initials == null || x.FullName.ToLower().Contains(filter.Initials.ToLower())));

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
