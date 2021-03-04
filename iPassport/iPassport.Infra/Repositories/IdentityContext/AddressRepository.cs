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
    public class AddressRepository : IAddressRepository, IDisposable
    {
        private readonly PassportIdentityContext _context;

        public AddressRepository(PassportIdentityContext context) => _context = context;

        public async Task<Address> FindById(Guid id) =>
            await _context.Address.Where(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<List<Address>> FindAll() =>
            await _context.Address.ToListAsync();

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
