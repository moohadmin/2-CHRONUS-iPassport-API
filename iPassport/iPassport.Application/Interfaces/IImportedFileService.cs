using iPassport.Application.Models;
using iPassport.Application.Models.Pagination;
using iPassport.Domain.Filters;
using System;
using System.Threading.Tasks;

namespace iPassport.Application.Interfaces
{
    public interface IImportedFileService
    {
        Task<PagedResponseApi> FindByPeriod(GetImportedFileFilter filter);
        Task<ResponseApi> GetImportedFileDetails(Guid fileId);
    }
}
