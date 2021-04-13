using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories.PassportIdentityContext;
using iPassport.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Infra.Repositories.IdentityContext
{
    public class CityRepository : IdentityBaseRepository<City>, ICityRepository
    {
        public CityRepository(PassportIdentityContext context) : base(context) { }

        public async Task<PagedData<City>> FindByStateAndNameParts(GetByIdAndNamePartsPagedFilter filter)
        {
            var query = _DbSet.Where(m => m.StateId == filter.Id &&
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
