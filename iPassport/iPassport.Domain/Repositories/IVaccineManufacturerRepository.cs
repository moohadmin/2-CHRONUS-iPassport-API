using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories
{
    public interface IVaccineManufacturerRepository : IRepository<VaccineManufacturer>
    {
        Task<PagedData<VaccineManufacturer>> GetByNameInitals(GetByNameInitalsPagedFilter filter);
    }
}
