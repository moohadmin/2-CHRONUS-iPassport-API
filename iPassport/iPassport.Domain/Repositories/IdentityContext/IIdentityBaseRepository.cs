using iPassport.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories
{
    public interface IIdentityBaseRepository<T>: IDisposable where T : Entity
    {
        Task<IList<T>> FindAll();
        Task<T> Find(Guid id);
        Task<bool> InsertAsync(T obj);
        Task<bool> Update(T obj);
        Task<bool> Delete(T obj);
    }
}
