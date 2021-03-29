using iPassport.Application.Models.Pagination;
using iPassport.Domain.Filters;
using System.Threading.Tasks;

namespace iPassport.Application.Interfaces
{
    public interface IGenderService
    {
        Task<PagedResponseApi> FindByNameParts(GetByNamePartsPagedFilter filter);
    }
}
