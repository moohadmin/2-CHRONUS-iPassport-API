using iPassport.Domain.Entities;
using iPassport.Domain.Enums;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories
{
    public interface IVaccinePeriodTypeRepository : IRepository<VaccinePeriodType>
    {
        public Task<VaccinePeriodType> GetByIdentifyer(EVaccinePeriodType identifyer);
    }
}
