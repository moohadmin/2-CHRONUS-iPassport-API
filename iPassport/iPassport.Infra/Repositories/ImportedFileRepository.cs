using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories;
using iPassport.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Infra.Repositories
{
    public class ImportedFileRepository : Repository<ImportedFile>, IImportedFileRepository
    {
        public ImportedFileRepository(iPassportContext context) : base(context) { }

        public async Task<PagedData<ImportedFile>> FindByPeriod(GetImportedFileFilter filter)
        {
            var query = _DbSet.Where(x => x.CreateDate.Date >= filter.StartTime.Date && x.CreateDate.Date <= filter.EndTime.Date)
                .Include(y => y.ImportingUser).Include(z => z.ImportedFileDetails).OrderBy(m => m.Name);

            return await Paginate(query, filter);
        }
    }
}
