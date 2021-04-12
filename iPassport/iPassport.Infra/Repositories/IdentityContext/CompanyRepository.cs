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

        public async Task<PagedData<Company>> FindByNameParts(GetByNamePartsPagedFilter filter)
        {
            var query = _DbSet.Where(m => string.IsNullOrWhiteSpace(filter.Initials) || m.Name.ToLower().Contains(filter.Initials.ToLower())).OrderBy(m => m.Name);

            return await Paginate(query, filter);
        }

        public async Task<Company> GetLoadedCompanyById(Guid id) =>
            await _DbSet
            .Include(x => x.Address).ThenInclude(x => x.City).ThenInclude(x => x.State).ThenInclude(x => x.Country)
            .Include(x=> x.Segment).ThenInclude(x => x.CompanyType)        
            .Include(x => x.ParentCompany)
            .Include(x => x.Responsible)
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

        private IQueryable<Company> GetLoadedHeadquarters() =>
            _DbSet.Include(x => x.Address).ThenInclude(x => x.City).ThenInclude(x => x.State).ThenInclude(x => x.Country)
                  .Include(x => x.Segment).ThenInclude(x => x.CompanyType);
    }
}
