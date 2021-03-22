using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories
{
    public interface IImportedFileRepository : IRepository<ImportedFile>
    {
        public Task<PagedData<ImportedFile>> FindByPeriod(GetImportedFileFilter filter);
    }
}
