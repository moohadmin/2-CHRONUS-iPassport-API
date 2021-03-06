using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories.PassportIdentityContext;
using iPassport.Infra.Contexts;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Infra.Repositories.IdentityContext
{
    public class StateRepository : IdentityBaseRepository<State>, IStateRepository
    {
        public StateRepository(PassportIdentityContext context) : base(context) { }

        public async Task<PagedData<State>> GetByCountryId(GetByIdPagedFilter filter)
        {
            var query = _DbSet
                .Where(m => m.CountryId == filter.Id)
                .OrderBy(m => m.Name);

            return await Paginate(query, filter);
        }
    }
}
