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
    public class CityRepository : ICityRepository, IDisposable
    {
        private readonly PassportIdentityContext _context;

        public CityRepository(PassportIdentityContext context) => _context = context;

        public async Task<City> FindById(Guid id) =>
            await _context.City.Where(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<List<City>> FindAll() =>
            await _context.City.ToListAsync();

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
