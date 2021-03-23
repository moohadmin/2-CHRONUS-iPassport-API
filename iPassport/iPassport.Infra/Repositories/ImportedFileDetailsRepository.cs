using iPassport.Domain.Entities;
using iPassport.Domain.Repositories;
using iPassport.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Infra.Repositories
{
    public class ImportedFileDetailsRepository : Repository<ImportedFileDetails>, IImportedFileDetailsRepository
    {
        public ImportedFileDetailsRepository(iPassportContext context) : base(context) { }

        public async Task<IList<ImportedFileDetails>> GetByFileId(Guid id) =>
            await _DbSet.Where(x => x.ImportedFileId == id).ToListAsync();
    }
}
