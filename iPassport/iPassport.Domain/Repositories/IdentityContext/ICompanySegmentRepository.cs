using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using System;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories.PassportIdentityContext
{
    public interface ICompanySegmentRepository : IIdentityBaseRepository<CompanySegment>
    {
        Task<PagedData<CompanySegment>> GetPagedByTypeId(Guid id, PageFilter filter);

        Task<CompanySegment> GetLoaded(Guid id);
    }
}
