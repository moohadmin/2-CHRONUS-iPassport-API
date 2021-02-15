using iPassport.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        Task<IList<T>> FindAll();
        Task<T> Find(Guid id);
        Task InsertAsync(T obj);
        void Update(T obj);
        void Delete(T obj);
    }
}
