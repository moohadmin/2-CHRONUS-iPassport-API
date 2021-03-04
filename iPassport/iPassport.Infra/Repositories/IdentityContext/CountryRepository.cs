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
    public class CountryRepository : ICountryRepository, IDisposable
    {
        private readonly PassportIdentityContext _context;

        public CountryRepository(PassportIdentityContext context) => _context = context;

        public async Task<Country> FindById(Guid id) =>
            await _context.Country.Where(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<List<Country>> FindAll() =>
            await _context.Country.ToListAsync();

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
