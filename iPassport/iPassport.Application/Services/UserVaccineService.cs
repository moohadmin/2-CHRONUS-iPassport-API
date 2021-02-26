using AutoMapper;
using iPassport.Application.Extensions;
using iPassport.Application.Interfaces;
using iPassport.Application.Models.Pagination;
using iPassport.Application.Models.ViewModels;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Application.Services
{
    public class UserVaccineService : IUserVaccineService
    {
        private readonly IMapper _mapper;
        private readonly IUserVaccineRepository _repository;
        private readonly IHttpContextAccessor _accessor;

        public UserVaccineService(IMapper mapper, IUserVaccineRepository repository, IHttpContextAccessor accessor)
        {
            _mapper = mapper;
            _repository = repository;
            _accessor = accessor;
        }

        public async Task<PagedResponseApi> GetUserVaccines(PageFilter pageFilter)
        {
            var res = await _repository.GetPagedUserVaccines(_accessor.GetCurrentUserId(), pageFilter);

            var result = _mapper.Map<IList<UserVaccineViewModel>>(res.Data).GroupBy(r => r.Manufacturer);

            return new PagedResponseApi(true, "vacinas do usuário", res.PageNumber, res.PageSize, res.TotalPages, res.TotalRecords, result);
        }
    }
}
