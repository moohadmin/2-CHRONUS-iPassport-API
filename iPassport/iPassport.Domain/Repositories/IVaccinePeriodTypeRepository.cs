using iPassport.Domain.Entities;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories
{
    public interface IVaccinePeriodTypeRepository : IRepository<VaccinePeriodType>
    {
        public Task<VaccinePeriodType> GetByIdentifyer(int identifyer);
    }
}
