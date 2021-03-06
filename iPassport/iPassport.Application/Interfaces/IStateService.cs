using iPassport.Application.Models;
using iPassport.Application.Models.Pagination;
using iPassport.Domain.Dtos;
using iPassport.Domain.Filters;
using System.Threading.Tasks;

namespace iPassport.Application.Interfaces
{
    public interface IStateService
    {
        Task<PagedResponseApi> GetByCountryId(GetByIdPagedFilter filter);
        Task<ResponseApi> Add(StateCreateDto dto);
    }
}
