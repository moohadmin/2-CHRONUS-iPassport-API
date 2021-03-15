using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories.PassportIdentityContext;
using iPassport.Infra.Contexts;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Infra.Repositories.IdentityContext
{
    public class GenderRepository : IdentityBaseRepository<Gender>, IGenderRepository
    {
        public GenderRepository(PassportIdentityContext context) : base(context) { }

        public async Task<PagedData<Gender>> FindByNameParts(GetByNamePartsPagedFilter filter)
        {
            var query = _DbSet.Where(m => string.IsNullOrWhiteSpace(filter.Initials) || m.Name.ToLower().Contains(filter.Initials.ToLower())).OrderBy(m => m.Name);

            return await Paginate(query, filter);
        }
    }
}
