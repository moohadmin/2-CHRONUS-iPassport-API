using iPassport.Domain.Entities;
using iPassport.Domain.Repositories.PassportIdentityContext;
using iPassport.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Infra.Repositories.IdentityContext
{
    public class AddressRepository : IdentityBaseRepository<Address>, IAddressRepository
    {
        public AddressRepository(PassportIdentityContext context) : base(context) { }

        public async Task<Address> FindFullAddress(System.Guid id) =>
            await _DbSet.Where(x => x.Id == id)
                    .Include(x => x.City).ThenInclude( y => y.State).ThenInclude(z => z.Country)
                    .FirstOrDefaultAsync();
    }
}
