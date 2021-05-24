using iPassport.Domain.Entities;
using iPassport.Domain.Repositories;
using iPassport.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace iPassport.Infra.Repositories
{
    public class VaccineDosageTypeRepository : Repository<VaccineDosageType>, IVaccineDosageTypeRepository
    {
        public VaccineDosageTypeRepository(iPassportContext context) : base(context) { }

        public async Task<VaccineDosageType> GetByIdentifyer(int identifyer)
            => await _DbSet.FirstOrDefaultAsync(x => x.Identifyer == identifyer);
    }
}
