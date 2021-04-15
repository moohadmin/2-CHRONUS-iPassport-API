using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Enums;
using iPassport.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Domain.Repositories.PassportIdentityContext
{
    public interface ICompanyRepository : IIdentityBaseRepository<Company>
    {
        Task<PagedData<CompanyAssociatedDto>> FindByNameParts(GetCompaniesPagedFilter filter);
        Task<Company> GetLoadedCompanyById(Guid id);
        Task<IList<Company>> FindListCnpj(List<string> listCnpj);
        Task<IList<Company>> GetPrivateHeadquarters(string cnpj, int segmentType);
        Task<IList<Company>> GetPublicMunicipalHeadquarters(Guid stateId, Guid countryId);
        Task<IList<Company>> GetPublicStateHeadquarters(Guid countryId);
        Task<bool> HasSubsidiariesCandidatesToFederalGovernment(Guid countryId);
        Task<bool> HasSubsidiariesCandidatesToStateGovernment(Guid stateId);
        Task<bool> HasSameSegmentAndLocaleGovernmentCompany(Guid localId, ECompanySegmentType segmentType);
        Task<bool> CnpjAlreadyRegistered(string cnpj);
        Task<PagedData<Company>> GetSubsidiariesCandidatesToFederalGovernmentPaged(Guid countryId, PageFilter filter);
        Task<PagedData<Company>> GetSubsidiariesCandidatesToStateGovernmentPaged(Guid stateId,PageFilter filter);
    }
}