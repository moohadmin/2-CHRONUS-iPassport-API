using iPassport.Application.Models;
using iPassport.Domain.Filters;
using System.Threading.Tasks;

namespace iPassport.Application.Interfaces
{
    public interface IIndicatorService
    {
        Task<ResponseApi> GetVaccinatedCount(GetVaccinatedCountFilter filter);
    }
}
