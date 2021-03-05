using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories
{
    public interface IDiseaseRepository : IRepository<Disease>
    {
        Task<PagedData<Disease>> GetByNameInitals(GetByNameInitialsPagedFilter filter);
    }
}
