using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Enums;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories.PassportIdentityContext;
using iPassport.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Infra.Repositories.IdentityContext
{
    public class CompanyRepository : IdentityBaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(PassportIdentityContext context) : base(context) { }

        public async Task<PagedData<CompanyAssociatedDto>> FindByNameParts(GetCompaniesPagedFilter filter, AccessControlDTO accessControl)
        {
            var query = AccessControllBaseQuery(accessControl)
                    .Include(x => x.Address).ThenInclude(x => x.City).ThenInclude(x => x.State).ThenInclude(x => x.Country)
                    .Include(x => x.Segment)
                    .Where(m => (string.IsNullOrWhiteSpace(filter.Initials) || m.Name.ToLower().Contains(filter.Initials.ToLower()))
                                && (filter.CityId == null || m.Address.CityId == filter.CityId)
                                && (filter.StateId == null || m.Address.City.StateId == filter.StateId)
                                && (filter.CountryId == null || m.Address.City.State.CountryId == filter.CountryId)
                                && (filter.Cnpj == null || m.Cnpj == filter.Cnpj)
                                && (filter.SegmentId == null || m.SegmentId == filter.SegmentId)
                                && (filter.TypeId == null || m.Segment.CompanyTypeId == filter.TypeId))
                    .OrderBy(m => m.Name)
                    .Select(x => 
                        new CompanyAssociatedDto(x, 
                            x.DeactivationDate == null &&
                                (x.Segment.Identifyer == (int)ECompanySegmentType.Federal 
                                    || x.Segment.Identifyer == (int)ECompanySegmentType.State) 
                            && (x.Segment.Identifyer == (int)ECompanySegmentType.Federal ?
                           QuerySubsidiariesCandidatesToGovernment().Any(y => y.Address.City.State.CountryId == x.Address.City.State.CountryId)
                                    : QuerySubsidiariesCandidatesToGovernment().Any(y => y.Address.City.StateId == x.Address.City.StateId && y.Segment.Identifyer == (int)ECompanySegmentType.Municipal))
                            ));

            return await PaginateCompanyDto(query, filter);
        }

        public async Task<Company> GetLoadedCompanyById(Guid id) =>
            await _DbSet
            .Include(x => x.Address).ThenInclude(x => x.City).ThenInclude(x => x.State).ThenInclude(x => x.Country)
            .Include(x => x.Segment).ThenInclude(x => x.CompanyType)
            .Include(x => x.ParentCompany)
            .Include(x => x.Subsidiaries)
            .Include(x => x.Responsible)
            .Include(x => x.DeactivationUser)
            .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<IList<Company>> FindListCnpj(List<string> listCnpj)
            => await _DbSet.Where(m => listCnpj.Contains(m.Cnpj)).ToListAsync();

        public async Task<IList<Company>> GetPrivateHeadquarters(string cnpj, int segmentType, AccessControlDTO accessControl) =>
            await GetLoadedHeadquarters(accessControl).Where(x => x.Cnpj.StartsWith(cnpj)
                            && x.Segment.Identifyer == segmentType
                            && x.Segment.CompanyType.Identifyer == (int)ECompanyType.Private
                            && x.ParentId == null).ToListAsync();

        public async Task<IList<Company>> GetPublicMunicipalHeadquarters(Guid stateId, Guid countryId, AccessControlDTO accessControl) =>
            await GetLoadedHeadquarters(accessControl).Where(x => (x.Segment.Identifyer == (int)ECompanySegmentType.State && x.Address.City.StateId == stateId)
                                                     || (x.Segment.Identifyer == (int)ECompanySegmentType.Federal && x.Address.City.State.CountryId == countryId)).ToListAsync();

        public async Task<IList<Company>> GetPublicStateHeadquarters(Guid countryId, AccessControlDTO accessControl) =>
            await GetLoadedHeadquarters(accessControl).Where(x => x.Segment.Identifyer == (int)ECompanySegmentType.Federal
                            && x.Address.City.State.CountryId == countryId
                            && x.Segment.CompanyType.Identifyer == (int)ECompanyType.Government).ToListAsync();

        public async Task<bool> HasSubsidiariesCandidatesToFederalGovernment(Guid countryId) =>
            await QuerySubsidiariesCandidatesToFederalGovernment(countryId).AnyAsync();

        public async Task<bool> HasSubsidiariesCandidatesToStateGovernment(Guid stateId) =>
            await QuerySubsidiariesCandidatesToStateGovernment(stateId).AnyAsync();

        public async Task<bool> HasSameSegmentAndLocaleGovernmentCompany(Guid localId, ECompanySegmentType segmentType, Guid? changedCompanyId)
        {
            var segmentIdentifyer = (int)segmentType;
            var query = _DbSet.Where(x => x.Segment.CompanyType.Identifyer == (int)ECompanyType.Government && x.Segment.Identifyer == segmentIdentifyer);
            
            if(changedCompanyId.HasValue)
                query = _DbSet.Where(x => x.Id != changedCompanyId.Value);

            query = segmentType switch
            {
                ECompanySegmentType.Municipal => query.Where(x => x.Address.CityId == localId),
                ECompanySegmentType.State => query.Where(x => x.Address.City.StateId == localId),
                ECompanySegmentType.Federal => query.Where(x => x.Address.City.State.CountryId == localId),
                _ => query,
            };

            return await query.AnyAsync();
        }

        public async Task<bool> CnpjAlreadyRegistered(string cnpj)
            => await _DbSet.AnyAsync(x => x.Cnpj.Equals(cnpj));

        public async Task<PagedData<Company>> GetSubsidiariesCandidatesToFederalGovernmentPaged(Guid countryId, PageFilter filter)
            => await Paginate(QuerySubsidiariesCandidatesToFederalGovernment(countryId).Include(x => x.Segment).OrderBy(m => m.Name), filter);

        public async Task<IList<Company>> GetSubsidiariesCandidatesToFederalGovernment(Guid countryId, IEnumerable<Guid> candidates)
            => await QuerySubsidiariesCandidatesToFederalGovernment(countryId)
                            .Where(x => (candidates == null || !candidates.Any()) || candidates.Contains(x.Id))
                            .OrderBy(m => m.Name)
                            .ToListAsync();

        public async Task<PagedData<Company>> GetSubsidiariesCandidatesToStateGovernmentPaged(Guid stateId, PageFilter filter)
            => await Paginate(QuerySubsidiariesCandidatesToStateGovernment(stateId).Include(x => x.Segment).OrderBy(m => m.Name), filter);

        public async Task<IList<Company>> GetSubsidiariesCandidatesToStateGovernment(Guid stateId, IEnumerable<Guid> candidates)
            => await QuerySubsidiariesCandidatesToStateGovernment(stateId)
                            .Where(x => (candidates == null || !candidates.Any()) || candidates.Contains(x.Id))
                            .OrderBy(m => m.Name)
                            .ToListAsync();

        #region Private
        private IQueryable<Company> GetLoadedHeadquarters(AccessControlDTO accessControl) =>
            AccessControlHeadquartersQuery(accessControl).Include(x => x.Address).ThenInclude(x => x.City).ThenInclude(x => x.State).ThenInclude(x => x.Country)
                  .Include(x => x.Segment).ThenInclude(x => x.CompanyType)
                  .Where(x => x.DeactivationDate == null);

        private IQueryable<Company> QuerySubsidiariesCandidatesToFederalGovernment(Guid countryId) =>
            QuerySubsidiariesCandidatesToGovernment().Where(x => x.Address.City.State.CountryId == countryId);
       
        private IQueryable<Company> QuerySubsidiariesCandidatesToStateGovernment(Guid stateId) =>
            QuerySubsidiariesCandidatesToGovernment().Where(x => x.Address.City.StateId == stateId 
                                    && x.Segment.Identifyer == (int)ECompanySegmentType.Municipal);

        private IQueryable<Company> QuerySubsidiariesCandidatesToGovernment() 
            =>
            _DbSet.Where(x => x.ParentId == null && x.DeactivationDate == null
                             && x.Segment.CompanyType.Identifyer == (int)ECompanyType.Government                             
                             && (x.Segment.Identifyer == (int)ECompanySegmentType.Municipal
                                    || x.Segment.Identifyer == (int)ECompanySegmentType.State));

        private async Task<PagedData<CompanyAssociatedDto>> PaginateCompanyDto(IQueryable<CompanyAssociatedDto> dbSet, PageFilter filter)
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

            return new PagedData<CompanyAssociatedDto>() { PageNumber = filter.PageNumber, PageSize = filter.PageSize, TotalPages = totalPages, TotalRecords = dataCount, Data = data };
        }

        private IQueryable<Company> AccessControllBaseQuery(AccessControlDTO accessControl)
        {
            var query = _DbSet.AsQueryable();

            if (accessControl.Profile == EProfileKey.business.ToString() && accessControl.CompanyId.HasValue && accessControl.CompanyId.Value != Guid.Empty)
                query = query.Where(c => c.Id == accessControl.CompanyId || c.ParentId == accessControl.CompanyId);

            if(accessControl.Profile == EProfileKey.government.ToString())
            {
                query = query.Where(x => x.Segment.CompanyType.Identifyer == (int)ECompanyType.Government);

                if (accessControl.CityId.HasValue && accessControl.CityId.Value != Guid.Empty)
                    query = query.Where(x => x.Address.CityId == accessControl.CityId.Value && x.Segment.Identifyer == (int)ECompanySegmentType.Municipal);

                if (accessControl.StateId.HasValue && accessControl.StateId.Value != Guid.Empty)
                    query = query.Where(x => x.Address.City.StateId == accessControl.StateId.Value && x.Segment.Identifyer <= (int)ECompanySegmentType.State);

                if (accessControl.CountryId.HasValue && accessControl.CountryId.Value != Guid.Empty)
                    query = query.Where(x => x.Address.City.State.CountryId == accessControl.CountryId.Value && x.Segment.Identifyer <= (int)ECompanySegmentType.Federal);
            }
            return query;
        }
        private IQueryable<Company> AccessControlHeadquartersQuery(AccessControlDTO accessControl)
        {
            var query = _DbSet.AsQueryable();

            if (accessControl.Profile == EProfileKey.business.ToString() && accessControl.CompanyId.HasValue && accessControl.CompanyId.Value != Guid.Empty)
                query = query.Where(c => c.Id == accessControl.CompanyId || c.ParentId == accessControl.CompanyId);

            
            return query;
        }
        #endregion
    }
}
