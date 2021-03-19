using iPassport.Application.Models;
using iPassport.Application.Models.Pagination;
using iPassport.Domain.Dtos;
using iPassport.Domain.Filters;
using System.Threading.Tasks;

namespace iPassport.Application.Interfaces
{
    public interface IHealthUnitService
    {
        Task<PagedResponseApi> FindByNameParts(GetByNamePartsPagedFilter filter);
        Task<ResponseApi> Add(HealthUnitCreateDto dto);
    }
}
