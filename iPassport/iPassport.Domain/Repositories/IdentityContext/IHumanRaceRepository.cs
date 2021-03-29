using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories.PassportIdentityContext
{
    public interface IHumanRaceRepository : IIdentityBaseRepository<HumanRace>
    {
        Task<PagedData<HumanRace>> FindByNameParts(GetByNamePartsPagedFilter filter);
        Task<IList<HumanRace>> FindByListName(List<string> listName);
    }
}