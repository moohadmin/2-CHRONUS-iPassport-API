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
    public class GenderRepository : IdentityBaseRepository<Gender>, IGenderRepository
    {
        public GenderRepository(PassportIdentityContext context) : base(context) { }

        public async Task<PagedData<Gender>> FindByNameParts(GetByNamePartsPagedFilter filter)
        {
            var query = _DbSet.Where(m => string.IsNullOrWhiteSpace(filter.Initials) || m.Name.ToLower().Contains(filter.Initials.ToLower())).OrderBy(m => m.Name);

            return await Paginate(query, filter);
        }

        public async Task<IList<Gender>> FindByListNames(List<string> names)
            => await _DbSet.Where(m => names.Contains(m.Name.ToUpper())).ToListAsync();
    }
}
