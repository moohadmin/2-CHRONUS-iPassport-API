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
    public class CityRepository : IdentityBaseRepository<City>, ICityRepository
    {
        public CityRepository(PassportIdentityContext context) : base(context) { }

        private IQueryable<City> AccessControllBaseQuery(AccessControlDTO accessControl)
        {
            var query = _DbSet.AsQueryable();

            if (accessControl.Profile == EProfileKey.government.ToString())
            {
                if (accessControl.CityId.HasValue && accessControl.CityId.Value != Guid.Empty)
                    query = query.Where(c => c.Id == accessControl.CityId.Value);

                if (accessControl.StateId.HasValue && accessControl.StateId.Value != Guid.Empty)
                    query = query.Where(c => c.StateId == accessControl.StateId.Value);

                if (accessControl.CountryId.HasValue && accessControl.CountryId.Value != Guid.Empty)
                    query = query.Where(c => c.State.CountryId == accessControl.CountryId.Value);
            }
            else if (accessControl.Profile == EProfileKey.healthUnit.ToString())
            {
                query = query.Where(c => c.Id == accessControl.CityId.Value);
            }

            return query;
        }

        public async Task<PagedData<City>> FindByStateAndNameParts(GetByIdAndNamePartsPagedFilter filter, AccessControlDTO accessControl)
        {
            var query = AccessControllBaseQuery(accessControl);

            query = query.Where(m => m.StateId == filter.Id &&
                (string.IsNullOrWhiteSpace(filter.Initials) || m.Name.ToLower().Contains(filter.Initials.ToLower())))
                .OrderBy(m => m.Name);

            return await Paginate(query, filter);
        }

        public async Task<IList<City>> FindByCityStateAndCountryNames(List<string> filter)
            => await _DbSet.Include(c => c.State).ThenInclude(s => s.Country)
                .Where(m => filter.Any(f => f == m.Name.ToUpper() + m.State.Name.ToUpper() + m.State.Country.Name.ToUpper())).ToListAsync();

        public async Task<City> FindLoadedById(System.Guid id) => await _DbSet.Include(x => x.State).ThenInclude(y => y.Country).Where(x => x.Id == id).FirstOrDefaultAsync();
    }
}
