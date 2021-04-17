using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Enums;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories.PassportIdentityContext;
using iPassport.Infra.Contexts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Infra.Repositories.IdentityContext
{
    public class CountryRepository : IdentityBaseRepository<Country>, ICountryRepository
    {
        public CountryRepository(PassportIdentityContext context) : base(context) { }
        
        private IQueryable<Country> AccessControllBaseQuery(AccessControlDTO accessControl)
        {
            var query = _DbSet.AsQueryable();

            if (accessControl.Profile == EProfileKey.government.ToString())
            {
                if (accessControl.CityId.HasValue && accessControl.CityId.Value != Guid.Empty)
                    query = query.Where(c => c.States.Any(s => s.Cities.Any(ci => ci.Id == accessControl.CityId.Value)));

                if (accessControl.StateId.HasValue && accessControl.StateId.Value != Guid.Empty)
                    query = query.Where(c => c.States.Any(s => s.Id == accessControl.StateId.Value));

                if (accessControl.CountryId.HasValue && accessControl.CountryId.Value != Guid.Empty)
                    query = query.Where(c => c.Id == accessControl.CountryId.Value);
            }
            else if (accessControl.Profile == EProfileKey.healthUnit.ToString())
            {
                query = query.Where(s => s.Id == accessControl.StateId.Value);
            }

            return query;
        }

        public async Task<PagedData<Country>> FindByNameParts(GetByNamePartsPagedFilter filter, AccessControlDTO accessControl)
        {
            var query = AccessControllBaseQuery(accessControl);
            query = query.Where(m => string.IsNullOrWhiteSpace(filter.Initials) || m.Name.ToLower().Contains(filter.Initials.ToLower())).OrderBy(m => m.Name);
            return await Paginate(query, filter);
        }
    }
}
