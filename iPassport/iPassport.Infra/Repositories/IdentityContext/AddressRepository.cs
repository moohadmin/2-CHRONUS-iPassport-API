using iPassport.Domain.Entities;
using iPassport.Domain.Repositories.PassportIdentityContext;
using iPassport.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public async Task<IList<Guid>> GetCityAddresses(Guid id) =>
            await _DbSet.Where(x => x.CityId == id).Select(x => x.Id).ToListAsync();

        public async Task<IList<Guid>> GetStateAddresses(Guid id) =>
            await _DbSet.Include(x => x.City).Where(x => x.City.StateId == id).Select(x => x.Id).ToListAsync();
        
        public async Task<IList<Guid>> GetCountryAddresses(Guid id) =>
            await _DbSet.Include(x => x.City).ThenInclude(x => x.State).Where(x => x.City.State.CountryId == id).Select(x => x.Id).ToListAsync();
    }
}
