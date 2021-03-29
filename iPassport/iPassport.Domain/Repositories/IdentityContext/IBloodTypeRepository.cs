using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories.PassportIdentityContext
{
    public interface IBloodTypeRepository : IIdentityBaseRepository<BloodType>
    {
        Task<PagedData<BloodType>> FindByNameParts(GetByNamePartsPagedFilter filter);
        Task<IList<BloodType>> FindByListName(List<string> listName);
    }
}