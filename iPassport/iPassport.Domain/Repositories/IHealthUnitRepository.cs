using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories
{
    public interface IHealthUnitRepository : IRepository<HealthUnit>
    {
        Task<PagedData<HealthUnit>> GetPagedHealthUnits(GetHealthUnitPagedFilter filter);
        Task<HealthUnit> GetByCnpj(string cnpj);
        Task<IList<HealthUnit>> FindByCnpjIneAndCode(List<string> listCnpj, List<string> listIne, List<int?> listCode);
        Task<int> GetNexUniqueCodeValue();
        Task<HealthUnit> GetLoadedById(Guid id);
    }
}
