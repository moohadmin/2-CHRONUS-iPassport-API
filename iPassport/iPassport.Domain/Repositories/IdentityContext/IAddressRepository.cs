using iPassport.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories.PassportIdentityContext
{
    public interface IAddressRepository : IIdentityBaseRepository<Address>
    {
        Task<Address> FindFullAddress(Guid id);
        Task<IList<Guid>> GetCityAddresses(Guid id);
        Task<IList<Guid>> GetStateAddresses(Guid id);
        Task<IList<Guid>> GetCountryAddresses(Guid id);
    }
}
