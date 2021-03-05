using iPassport.Application.Models;
using iPassport.Application.Models.Pagination;
using iPassport.Domain.Dtos;
using iPassport.Domain.Filters;
using System.Threading.Tasks;

namespace iPassport.Application.Interfaces
{
    public interface ICountryService
    {
        Task<ResponseApi> Add(CountryCreateDto dto);
        Task<ResponseApi> FindById(System.Guid id);
        Task<PagedResponseApi> GetByNameParts(GetByNamePartsPagedFilter filter);
    }
}
