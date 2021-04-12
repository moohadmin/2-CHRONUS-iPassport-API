using iPassport.Domain.Entities;
using iPassport.Domain.Repositories.PassportIdentityContext;
using iPassport.Infra.Contexts;

namespace iPassport.Infra.Repositories.IdentityContext
{
    public class CompanyResponsibleRepository : IdentityBaseRepository<CompanyResponsible>, ICompanyResponsibleRepository
    {
        public CompanyResponsibleRepository(PassportIdentityContext context) : base(context) { }

    }
}
