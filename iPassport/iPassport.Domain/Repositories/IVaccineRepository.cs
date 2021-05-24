using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories
{
    public interface IVaccineRepository : IRepository<Vaccine>
    {
        Task<PagedData<Vaccine>> GetByManufacturerId(GetByIdAndNamePartsPagedFilter filter);
        Task<PagedData<Vaccine>> GetPagged(GetPagedVaccinesFilter filter);
        Task<IList<Vaccine>> GetByVaccineAndManufacturerNames(List<string> filter);
        Task<bool> AssociateDiseases(Vaccine vaccine, IList<Disease> diseases);
    }
}
