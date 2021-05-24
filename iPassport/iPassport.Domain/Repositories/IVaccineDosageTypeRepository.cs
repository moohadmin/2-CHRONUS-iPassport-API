using iPassport.Domain.Entities;
using iPassport.Domain.Enums;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories
{
    public interface IVaccineDosageTypeRepository : IRepository<VaccineDosageType>
    {
        public Task<VaccineDosageType> GetByIdentifyer(EVaccineDosageType identifyer);
    }
}
