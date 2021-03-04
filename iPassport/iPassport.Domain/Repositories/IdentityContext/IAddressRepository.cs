using iPassport.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories.PassportIdentityContext
{
    public interface IAddressRepository
    {
        Task<Address> FindById(Guid id);
        Task<List<Address>> FindAll();
    }
}
