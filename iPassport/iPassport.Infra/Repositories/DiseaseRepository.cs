using iPassport.Domain.Entities;
using iPassport.Domain.Repositories;
using iPassport.Infra.Contexts;

namespace iPassport.Infra.Repositories
{
    public class DiseaseRepository : Repository<Disease>, IDiseaseRepository
    {
        public DiseaseRepository(iPassportContext context) : base(context) { }
    }
}
