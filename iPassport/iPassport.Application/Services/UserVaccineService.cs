using AutoMapper;
using iPassport.Application.Extensions;
using iPassport.Application.Interfaces;
using iPassport.Application.Models.Pagination;
using iPassport.Application.Models.ViewModels;
using iPassport.Application.Resources;
using iPassport.Domain.Enums;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
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
        private readonly IStringLocalizer<Resource> _localizer;

        public UserVaccineService(IMapper mapper, IUserVaccineRepository repository, IHttpContextAccessor accessor, IStringLocalizer<Resource> localizer)
        {
            _mapper = mapper;
            _repository = repository;
            _accessor = accessor;
            _localizer = localizer;
        }

        public async Task<PagedResponseApi> GetUserVaccines(GetByIdPagedFilter pageFilter)
        {
            var res = await _repository.GetPagedUserVaccines(pageFilter);

            //foreach (var d in res.Data)
            //{
            //    d.Status = 
            //}

            var result = _mapper.Map<IList<UserVaccineViewModel>>(res.Data).GroupBy(r => r.Manufacturer);

            return new PagedResponseApi(true, _localizer["UserVaccines"], res.PageNumber, res.PageSize, res.TotalPages, res.TotalRecords, result);
        }
    }
}
