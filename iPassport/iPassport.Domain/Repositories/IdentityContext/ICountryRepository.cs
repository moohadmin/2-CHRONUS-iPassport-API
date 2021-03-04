using iPassport.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories.PassportIdentityContext
{
    public interface ICountryRepository
    {
        Task<Country> FindById(Guid id);
        Task<List<Country>> FindAll();
    }
}
