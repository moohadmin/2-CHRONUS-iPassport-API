using AutoMapper;
using iPassport.Application.Extensions;
using iPassport.Application.Interfaces;
using iPassport.Application.Models.Pagination;
using iPassport.Application.Models.ViewModels;
using iPassport.Application.Resources;
using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Application.Services
{
    public class UserDiseaseTestService : IUserDiseaseTestService
    {
        private readonly IMapper _mapper;
        private readonly IUserDiseaseTestRepository _repository;
        private readonly IStringLocalizer<Resource> _localizer;
        private readonly IHttpContextAccessor _accessor;
        private readonly IUserDetailsRepository _userDetailsRepository;

        public UserDiseaseTestService(IMapper mapper, IUserDiseaseTestRepository repository, IStringLocalizer<Resource> localizer, IHttpContextAccessor accessor, IUserDetailsRepository userDetailsRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _localizer = localizer;
            _accessor = accessor;
            _userDetailsRepository = userDetailsRepository;
        }

        public async Task<PagedResponseApi> GetCurrentUserDiseaseTest(PageFilter pageFilter)
        {
            var res = await _repository.GetPagedUserDiseaseTestsByUserId(new GetByIdPagedFilter(_accessor.GetCurrentUserId(), pageFilter));

            var result = _mapper.Map<IList<UserDiseaseTestViewModel>>(res.Data);
            
            await GetUserDiseaseTestStatus(result);

            return new PagedResponseApi(true, _localizer["UserVaccines"], res.PageNumber, res.PageSize, res.TotalPages, res.TotalRecords, result);
        }

        public async Task<PagedResponseApi> GetUserDiseaseTest(GetByIdPagedFilter pageFilter)
        {
            var res = await _repository.GetPaggedUserDiseaseTestsByPassportId(pageFilter);

            var result = _mapper.Map<IList<UserDiseaseTestViewModel>>(res.Data);

            await GetUserDiseaseTestStatus(result);

            return new PagedResponseApi(true, _localizer["UserVaccines"], res.PageNumber, res.PageSize, res.TotalPages, res.TotalRecords, result);

        }

        private async Task GetUserDiseaseTestStatus(IList<UserDiseaseTestViewModel> tests)
        {
            if (tests.Any())
            {
                var user = await _userDetailsRepository.GetLoadedUsersById(tests.FirstOrDefault().UserId);
                
                foreach (var t in tests)
                    t.Status = user.GetDiseaseTestStatus(t.Id);
            }
        }
    }
}
