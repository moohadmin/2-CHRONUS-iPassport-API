using AutoMapper;
using iPassport.Application.Extensions;
using iPassport.Application.Interfaces;
using iPassport.Application.Models.Pagination;
using iPassport.Application.Models.ViewModels;
using iPassport.Application.Resources;
using iPassport.Domain.Dtos;
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
        private readonly IUserDetailsRepository _userDetailsRepository;
        private readonly IStringLocalizer<Resource> _localizer;
        private readonly IHttpContextAccessor _accessor;

        public UserVaccineService(IMapper mapper, IUserVaccineRepository repository, IUserDetailsRepository userDetailsRepository, IStringLocalizer<Resource> localizer, IHttpContextAccessor accessor)
        {
            _mapper = mapper;
            _repository = repository;
            _localizer = localizer;
            _accessor = accessor;
            _userDetailsRepository = userDetailsRepository;
        }

        public async Task<PagedResponseApi> GetCurrentUserVaccines(PageFilter pageFilter)
        { 
            var res = await _repository.GetPagedUserVaccinesByUserId(new GetByIdPagedFilter(_accessor.GetCurrentUserId(), pageFilter));

            await GetVaccineDetailsStatus(res.Data);

            var result = _mapper.Map<IList<UserVaccineViewModel>>(res.Data);

            return new PagedResponseApi(true, _localizer["UserVaccines"], res.PageNumber, res.PageSize, res.TotalPages, res.TotalRecords, result);
        }

        public async Task<PagedResponseApi> GetUserVaccines(GetByIdPagedFilter pageFilter) { 
            var res = await _repository.GetPagedUserVaccinesByPassportId(pageFilter);

            await GetVaccineDetailsStatus(res.Data);

            var result = _mapper.Map<IList<UserVaccineViewModel>>(res.Data);

            return new PagedResponseApi(true, _localizer["UserVaccines"], res.PageNumber, res.PageSize, res.TotalPages, res.TotalRecords, result);

        }

        private async Task GetVaccineDetailsStatus(IList<UserVaccineDetailsDto> detailsDto)
        {
            if (detailsDto.Any())
            {
                var user = await _userDetailsRepository.GetLoadedUsersById(detailsDto.FirstOrDefault().UserId);
                foreach (var d in detailsDto)
                    d.Status = user.GetUserVaccineStatus(d.VaccineId);
            }
        }
    }
}
