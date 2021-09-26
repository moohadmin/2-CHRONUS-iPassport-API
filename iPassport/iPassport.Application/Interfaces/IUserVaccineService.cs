using iPassport.Application.Models.Pagination;
using iPassport.Application.Models.ViewModels;
using iPassport.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Application.Interfaces
{
    public interface IUserVaccineService
    {
        Task<PagedResponseApi> GetUserVaccines(GetByIdPagedFilter pageFilter);
        Task<IList<UserVaccineViewModel>> GetAllUserVaccines(Guid Id);
        Task<PagedResponseApi> GetCurrentUserVaccines(PageFilter pageFilter);
    }
}
