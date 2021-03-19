using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories
{

    public interface IHealthUnitRepository : IRepository<HealthUnit>
    {
        Task<PagedData<HealthUnit>> FindByNameParts(GetByNamePartsPagedFilter filter);
        Task<HealthUnit> GetByCnpj(string cnpj);
    }
}
