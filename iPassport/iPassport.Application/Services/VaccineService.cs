using AutoMapper;
using iPassport.Application.Extensions;
using iPassport.Application.Interfaces;
using iPassport.Application.Models.Pagination;
using iPassport.Application.Models.ViewModels;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Application.Services
{
    public class VaccineService : IVaccineService
    {
        private readonly IMapper _mapper;
        private readonly IVaccineRepository _repository;
        private readonly IHttpContextAccessor _accessor;

        public VaccineService(IMapper mapper, IVaccineRepository repository, IHttpContextAccessor accessor)
        {
            _mapper = mapper;
            _repository = repository;
            _accessor = accessor;
        }

        public async Task<PagedResponseApi> GetUserVaccines(PageFilter pageFilter)
        {
            var res = await _repository.GetPagedUserVaccines(_accessor.GetCurrentUserId(), pageFilter);

            var result = _mapper.Map<IList<VaccineViewModel>>(res.Data);

            return new PagedResponseApi(true, "vacinas do usuário" , res.PageNumber, res.PageSize, res.TotalPages,res.TotalRecords, result);
        }
    }
}
