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
    public class CompanySegmentRepository : IdentityBaseRepository<CompanySegment>, ICompanySegmentRepository
    {
        public CompanySegmentRepository(PassportIdentityContext context) : base(context) { }

        public async Task<PagedData<CompanySegment>> GetPagedByTypeId(Guid id, PageFilter filter)
        {
            var query = _DbSet
                .Where(m => m.CompanyTypeId == id)
                .OrderBy(m => m.Name);

            return await Paginate(query, filter);
        }

        public async Task<CompanySegment> GetLoaded(Guid id) =>
             await _DbSet.Include(x => x.CompanyType).Where(m => m.Id == id).FirstOrDefaultAsync();
                

    }
}
