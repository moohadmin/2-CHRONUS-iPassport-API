using iPassport.Domain.Entities;
using iPassport.Domain.Repositories.PassportIdentityContext;
using iPassport.Infra.Contexts;

namespace iPassport.Infra.Repositories.IdentityContext
{
    public class AddressRepository : IdentityBaseRepository<Address>, IAddressRepository
    {
        public AddressRepository(PassportIdentityContext context) : base(context) { }

    }
}
