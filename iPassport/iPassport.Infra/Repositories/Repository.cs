using iPassport.Domain.Entities;
using iPassport.Domain.Repositories;
using iPassport.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Infra.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly iPassportContext _context;
        protected readonly DbSet<T> _DbSet;

        public Repository(iPassportContext context)
        {
            _context = context;
            _DbSet = _context.Set<T>();
        }

        public virtual async Task<IList<T>> FindAll() => await _DbSet.ToListAsync();

        public virtual async Task<T> Find(Guid id) => await _DbSet.FindAsync(id);

        public virtual async Task InsertAsync(T obj)
        {
            _DbSet.Add(obj);
            await _context.SaveChangesAsync();
        }

        public void Update(T obj)
        {
            obj.SetUpdateDate();

            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(T obj)
        {
            _DbSet.Remove(obj);
            _context.SaveChanges();
        }
    }
}
