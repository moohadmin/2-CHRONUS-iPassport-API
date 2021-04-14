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

        public async Task<PagedData<Company>> FindByNameParts(GetCompaniesPagedFilter filter)
        {
            var query = _DbSet
                    .Include(x => x.Address)
                    .Include(x => x.Segment)
                    .Where(m => (string.IsNullOrWhiteSpace(filter.Initials) || m.Name.ToLower().Contains(filter.Initials.ToLower()))
                                && (filter.CityId == null || m.Address.CityId == filter.CityId)
                                && (filter.Cnpj == null || m.Cnpj == filter.Cnpj)
                                && (filter.SegmentId == null || m.SegmentId == filter.SegmentId)
                                && (filter.TypeId == null || m.Segment.CompanyTypeId == filter.TypeId))
                    .OrderBy(m => m.Name);

            return await Paginate(query, filter);
        }

        public async Task<Company> GetLoadedCompanyById(Guid id) =>
            await _DbSet
            .Include(x => x.Address).ThenInclude(x => x.City).ThenInclude(x => x.State).ThenInclude(x => x.Country)
            .Include(x => x.Segment).ThenInclude(x => x.CompanyType)
            .Include(x => x.ParentCompany)
            .Include(x => x.Responsible)
            .Include(x => x.DeactivationUser)
            .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<IList<Company>> FindListCnpj(List<string> listCnpj)
            => await _DbSet.Where(m => listCnpj.Contains(m.Cnpj)).ToListAsync();

        public async Task<IList<Company>> GetPrivateHeadquarters(string cnpj, int segmentType) =>
            await GetLoadedHeadquarters().Where(x => x.Cnpj.StartsWith(cnpj)
                            && x.Segment.Identifyer == segmentType
                            && x.Segment.CompanyType.Identifyer == (int)ECompanyType.Private
                            && x.ParentId == null).ToListAsync();

        public async Task<IList<Company>> GetPublicMunicipalHeadquarters(Guid stateId) =>
            await GetLoadedHeadquarters().Where(x => (x.Segment.Identifyer == (int)ECompanySegmentType.State
                                                        || x.Segment.Identifyer == (int)ECompanySegmentType.Federal)
                            && x.Address.City.StateId == stateId).ToListAsync();

        public async Task<IList<Company>> GetPublicStateHeadquarters(Guid countryId) =>
            await GetLoadedHeadquarters().Where(x => x.Segment.Identifyer == (int)ECompanySegmentType.Federal
                            && x.Address.City.State.CountryId == countryId
                            && x.Segment.CompanyType.Identifyer == (int)ECompanyType.Government).ToListAsync();

        public async Task<bool> HasBranchCompanyToAssociateInFederal(Guid countryId) =>
            await _DbSet.AnyAsync(x => x.ParentId == null
                && x.Segment.CompanyType.Identifyer == (int)ECompanyType.Government
                && (x.Segment.Identifyer == (int)ECompanySegmentType.State || x.Segment.Identifyer == (int)ECompanySegmentType.Municipal)
                && x.Address.City.State.CountryId == countryId);

        public async Task<bool> HasBranchCompanyToAssociateInState(Guid stateId) =>
            await _DbSet.AnyAsync(x => x.ParentId == null
                && x.Segment.CompanyType.Identifyer == (int)ECompanyType.Government
                && x.Segment.Identifyer == (int)ECompanySegmentType.Municipal
                && x.Address.City.StateId == stateId);

        private IQueryable<Company> GetLoadedHeadquarters() =>
            _DbSet.Include(x => x.Address).ThenInclude(x => x.City).ThenInclude(x => x.State).ThenInclude(x => x.Country)
                  .Include(x => x.Segment).ThenInclude(x => x.CompanyType);
        public async Task<bool> HasSameSegmentAndLocaleGovernmentCompany(Guid localId, ECompanySegmentType segmentType)
        {
            return segmentType switch
            {
                ECompanySegmentType.Municipal => await _DbSet.AnyAsync(x => x.Segment.CompanyType.Identifyer == (int)ECompanyType.Government && x.Segment.Identifyer == (int)ECompanySegmentType.Municipal && x.Address.CityId == localId),
                ECompanySegmentType.State => await _DbSet.AnyAsync(x => x.Segment.CompanyType.Identifyer == (int)ECompanyType.Government && x.Segment.Identifyer == (int)ECompanySegmentType.State && x.Address.City.StateId == localId),
                ECompanySegmentType.Federal => await _DbSet.AnyAsync(x => x.Segment.CompanyType.Identifyer == (int)ECompanyType.Government && x.Segment.Identifyer == (int)ECompanySegmentType.Federal && x.Address.City.State.CountryId == localId),
                _ => false,
            };
        }

        public async Task<bool> CnpjAlreadyRegistered(string cnpj)
            => await _DbSet.AnyAsync(x => x.Cnpj.Equals(cnpj));
    }
}
