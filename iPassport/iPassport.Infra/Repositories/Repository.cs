using iPassport.Application.Exceptions;
using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories;
using iPassport.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using Npgsql;
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

        public virtual async Task<bool> InsertAsync(T obj)
        {
            try
            {
                _DbSet.Add(obj);
                var result = await _context.SaveChangesAsync();

                return result > 0;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.GetType() == (typeof(PostgresException)))
                {
                    var key = ((PostgresException)ex.InnerException).ConstraintName.Split('_').Last();

                    throw new UniqueKeyException(key, ex);
                }

                throw new PersistenceException(ex);
            }
            catch (Exception ex)
            {
                throw new PersistenceException(ex);
            }
        }

        public async Task<bool> Update(T obj)
        {
            try
            {
                obj.SetUpdateDate();

                _context.Entry(obj).State = EntityState.Modified;
                var result = await _context.SaveChangesAsync();

                return result > 0;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.GetType() == (typeof(PostgresException)))
                {
                    var key = ((PostgresException)ex.InnerException).ConstraintName.Split('_').Last();

                    throw new UniqueKeyException(key, ex);
                }

                throw new PersistenceException(ex);
            }
            catch (Exception ex)
            {
                throw new PersistenceException(ex);
            }
        }

        public async Task<bool> Delete(T obj)
        {
            _DbSet.Remove(obj);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        protected virtual async Task<PagedData<T>> Paginate(IQueryable<T> dbSet, PageFilter filter)
        {
            (int take, int skip) = CalcPageOffset(filter);
            var dataCount = await dbSet.CountAsync();

            var data = await dbSet.Take(take).Skip(skip).ToListAsync();

            int totalPages = 0;
            if (dataCount < filter.PageSize)
            {
                totalPages = 1;
            }
            else
            {
                totalPages = dataCount / filter.PageSize;
                totalPages = dataCount % filter.PageSize > 0 ? totalPages + 1 : totalPages;
            }

            return new PagedData<T>() { PageNumber = filter.PageNumber, PageSize = filter.PageSize, TotalPages = totalPages, TotalRecords = dataCount, Data = data };
        }

        protected (int, int) CalcPageOffset(PageFilter filter)
        {
            int skip = (filter.PageNumber - 1) * filter.PageSize;
            int take = skip + filter.PageSize;
            return (take, skip);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
