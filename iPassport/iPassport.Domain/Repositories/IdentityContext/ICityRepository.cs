using iPassport.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories.PassportIdentityContext
{
    public interface ICityRepository
    {
        Task<City> FindById(Guid id);
        Task<List<City>> FindAll();
    }
}