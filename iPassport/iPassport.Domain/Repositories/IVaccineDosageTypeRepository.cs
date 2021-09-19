using iPassport.Domain.Entities;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories
{
    public interface IVaccineDosageTypeRepository : IRepository<VaccineDosageType>
    {
        public Task<VaccineDosageType> GetByIdentifyer(int identifyer);
    }
}
