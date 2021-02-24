using iPassport.Domain.Entities;
using iPassport.Domain.Repositories;
using iPassport.Infra.Contexts;

namespace iPassport.Infra.Repositories
{
    public class VaccineManufacturerRepository : Repository<VaccineManufacturer>, IVaccineManufacturerRepository
    {
        public VaccineManufacturerRepository(iPassportContext context) : base(context) { }
    }
}
