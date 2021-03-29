using iPassport.Application.Models;
using iPassport.Application.Models.Pagination;
using iPassport.Domain.Dtos;
using iPassport.Domain.Filters;
using System;
using System.Threading.Tasks;

namespace iPassport.Application.Interfaces
{
    public interface IHealthUnitService
    {
        Task<PagedResponseApi> FindByNameParts(GetHealthUnitPagedFilter filter);
        Task<ResponseApi> Add(HealthUnitCreateDto dto);
        Task<ResponseApi> GetById(Guid id);
        Task<ResponseApi> Edit(HealthUnitEditDto dto);
    }
}
