using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories.PassportIdentityContext;
using iPassport.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
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
            await _DbSet.Include(x => x.Address).ThenInclude(x => x.City).ThenInclude(x => x.State).ThenInclude(x => x.Country)
                    .FirstOrDefaultAsync(x => x.Id == id);
    }
}
