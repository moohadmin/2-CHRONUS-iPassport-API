using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories.PassportIdentityContext
{
    public interface ICompanyRepository : IIdentityBaseRepository<Company>
    {
        Task<PagedData<Company>> FindByNameParts(GetByNamePartsPagedFilter filter);
    }
}