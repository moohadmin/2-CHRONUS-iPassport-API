using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories;
using iPassport.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        protected async Task<PagedData<T>> Paginate(IQueryable<T> dbSet, PageFilter filter)
        {
            (int take, int skip) = CalcPageOffset(filter);

            var data = await dbSet.Take(take).Skip(skip).ToListAsync();
            var totalPages = data.Count / filter.PageSize;

            return new PagedData<T>() { PageNumber = filter.PageNumber, PageSize = filter.PageSize, TotalPages = totalPages, TotalRecords = data.Count, Data = data };
        }

        private static (int, int) CalcPageOffset(PageFilter filter)
        {
            int skip = (filter.PageNumber - 1) * filter.PageSize;
            int take = skip + filter.PageSize;
            return (take, skip);
        }
    }
}
