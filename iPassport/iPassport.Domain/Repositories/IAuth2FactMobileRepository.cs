using iPassport.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories
{
    public interface IAuth2FactMobileRepository : IRepository<Auth2FactMobile>
    {
        Task<Auth2FactMobile> FindByUserAndPin(Guid id, string pin);
    }
}
