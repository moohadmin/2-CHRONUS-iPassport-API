using iPassport.Application.Models;
using iPassport.Application.Models.Pagination;
using iPassport.Domain.Dtos;
using iPassport.Domain.Filters;
using System;
using System.Threading.Tasks;

namespace iPassport.Application.Interfaces
{
    public interface ICompanyService
    {
        Task<PagedResponseApi> FindByNameParts(GetByNamePartsPagedFilter filter);
        Task<ResponseApi> Add(CompanyCreateDto dto);
        Task<ResponseApi> GetById(Guid id);
        Task<ResponseApi> GetAllTypes();
        Task<ResponseApi> GetHeadquartersCompanies(GetHeadquarterCompanyFilter filter);
        Task<PagedResponseApi> GetSegmetsByTypeId(Guid TypeId, PageFilter filter);
    }
}
