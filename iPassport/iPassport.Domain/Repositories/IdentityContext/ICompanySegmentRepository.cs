using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using System;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories.PassportIdentityContext
{
    public interface ICompanySegmentRepository : IIdentityBaseRepository<CompanySegment>
    {
        Task<PagedData<CompanySegment>> GetPagedByTypeId(Guid id, PageFilter filter, AccessControlDTO accessControl);

        Task<CompanySegment> GetLoaded(Guid id);
    }
}
