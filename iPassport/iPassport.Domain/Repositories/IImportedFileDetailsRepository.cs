using iPassport.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories
{
    public interface IImportedFileDetailsRepository : IRepository<ImportedFileDetails>
    {
        Task<IList<ImportedFileDetails>> GetByFileId(Guid id);
    }
}
