using iPassport.Domain.Entities;
using iPassport.Domain.Enums;
using iPassport.Domain.Repositories;
using iPassport.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace iPassport.Infra.Repositories
{
    public class VaccinePeriodTypeRepository : Repository<VaccinePeriodType>, IVaccinePeriodTypeRepository
    {
        public VaccinePeriodTypeRepository(iPassportContext context) : base(context) { }

        public async Task<VaccinePeriodType> GetByIdentifyer(EVaccinePeriodType identifyer)
            => await _DbSet.FirstOrDefaultAsync(x => x.Identifyer == (int)identifyer);
    }
}
