using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories.PassportIdentityContext
{
    public interface ICompanyRepository : IIdentityBaseRepository<Company>
    {
        Task<PagedData<Company>> FindByNameParts(GetByNamePartsPagedFilter filter);
        Task<Company> GetLoadedCompanyById(Guid id);
        Task<IList<Company>> FindListCnpj(List<string> listCnpj);
        Task<PagedData<Company>> GetHeadquartersCompanies(GetHeadquarterCompanyFilter filter);
    }
}