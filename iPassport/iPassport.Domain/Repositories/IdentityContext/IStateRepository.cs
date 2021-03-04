using iPassport.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories.PassportIdentityContext
{
    public interface IStateRepository
    {
        Task<State> FindById(Guid id);
        Task<List<State>> FindAll();
    }
}
