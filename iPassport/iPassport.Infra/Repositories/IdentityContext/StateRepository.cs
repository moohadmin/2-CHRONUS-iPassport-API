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
    public class StateRepository : IStateRepository, IDisposable
    {
        private readonly PassportIdentityContext _context;

        public StateRepository(PassportIdentityContext context) => _context = context;

        public async Task<State> FindById(Guid id) =>
            await _context.State.Where(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<List<State>> FindAll() =>
            await _context.State.ToListAsync();

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
