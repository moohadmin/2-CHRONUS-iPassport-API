using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories.PassportIdentityContext
{
    public interface ICompanyRepository : IIdentityBaseRepository<Company>
    {
        Task<PagedData<Company>> FindByNameParts(GetCompaniesPagedFilter filter);
        Task<Company> GetLoadedCompanyById(Guid id);
        Task<IList<Company>> FindListCnpj(List<string> listCnpj);
        Task<IList<Company>> GetPrivateHeadquarters(string cnpj, int segmentType);
        Task<IList<Company>> GetPublicMunicipalHeadquarters(Guid stateId);
        Task<IList<Company>> GetPublicStateHeadquarters(Guid countryId);
        Task<bool> HasBranchCompanyToAssociateInFederal(Guid countryId);
        Task<bool> HasBranchCompanyToAssociateInState(Guid stateId);
    }
}