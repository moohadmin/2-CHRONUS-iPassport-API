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
    public class StateRepository : IdentityBaseRepository<State>, IStateRepository
    {
        public StateRepository(PassportIdentityContext context) : base(context) { }

        private IQueryable<State> AccessControllBaseQuery(AccessControlDTO accessControl)
        {
            var query = _DbSet.AsQueryable();

            if (accessControl.Profile == EProfileKey.government.ToString())
            {
                if (accessControl.CityId.HasValue && accessControl.CityId.Value != Guid.Empty)
                    query = query.Where(s => s.Cities.Any(c => c.Id == accessControl.CityId.Value));

                if (accessControl.StateId.HasValue && accessControl.StateId.Value != Guid.Empty)
                    query = query.Where(s => s.Id == accessControl.StateId.Value);

                if (accessControl.CountryId.HasValue && accessControl.CountryId.Value != Guid.Empty)
                    query = query.Where(s => s.CountryId == accessControl.CountryId.Value);
            }
            else if (accessControl.Profile == EProfileKey.healthUnit.ToString())
            {
                query = query.Where(s => s.Id == accessControl.StateId.Value);
            }

            return query;
        }

        public async Task<PagedData<State>> GetByCountryId(GetByIdPagedFilter filter, AccessControlDTO accessControl)
        {
            var query = AccessControllBaseQuery(accessControl);
            query = query.Where(m => m.CountryId == filter.Id)
                .OrderBy(m => m.Name);

            return await Paginate(query, filter);
        }
    }
}
