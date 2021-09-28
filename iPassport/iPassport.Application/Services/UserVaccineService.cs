using AutoMapper;
using iPassport.Application.Extensions;
using iPassport.Application.Interfaces;
using iPassport.Application.Models.Pagination;
using iPassport.Application.Models.ViewModels;
using iPassport.Application.Resources;
using iPassport.Domain.Dtos;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories;
using iPassport.Domain.Repositories.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using System;
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
        private readonly IUserRepository _userRepository;
        private readonly IStringLocalizer<Resource> _localizer;
        private readonly IHttpContextAccessor _accessor;

        public UserVaccineService(IMapper mapper,
            IUserVaccineRepository repository,
            IUserDetailsRepository userDetailsRepository,
            IUserRepository userRepository,
            IStringLocalizer<Resource> localizer,
            IHttpContextAccessor accessor)
        {
            _mapper = mapper;
            _repository = repository;
            _localizer = localizer;
            _accessor = accessor;
            _userDetailsRepository = userDetailsRepository;
            _userRepository = userRepository;
        }

        public async Task<IList<UserVaccineViewModel>> GetAllUserVaccines(Guid Id)
        {
            var user = await _userDetailsRepository.GetByPassportId(Id);
            var userBirthday = await  _userRepository.GetUserBirthdayDate(user.Id);

            GetByIdPagedFilter pageFilter = new GetByIdPagedFilter(Id, new PageFilter(0, 1000));

            var res = await _repository.GetPagedUserVaccinesByPassportId(pageFilter, userBirthday);

            await GetVaccineDetailsStatus(res.Data);

            var result = _mapper.Map<IList<UserVaccineViewModel>>(res.Data);

            return result;
        }

        public async Task<PagedResponseApi> GetCurrentUserVaccines(PageFilter pageFilter)
        {
            var userBirthday = await  _userRepository.GetUserBirthdayDate(_accessor.GetCurrentUserId());

            var res = await _repository.GetPagedUserVaccinesByUserId(new GetByIdPagedFilter(_accessor.GetCurrentUserId(), pageFilter), userBirthday);

            await GetVaccineDetailsStatus(res.Data);

            var result = _mapper.Map<IList<UserVaccineViewModel>>(res.Data);

            return new PagedResponseApi(true, _localizer["UserVaccines"], res.PageNumber, res.PageSize, res.TotalPages, res.TotalRecords, result);
        }

        public async Task<PagedResponseApi> GetUserVaccines(GetByIdPagedFilter pageFilter) {
            
            var user = await _userDetailsRepository.GetByPassportId(pageFilter.Id);
            var userBirthday = await  _userRepository.GetUserBirthdayDate(user.Id);

            var res = await _repository.GetPagedUserVaccinesByPassportId(pageFilter, userBirthday);

            await GetVaccineDetailsStatus(res.Data);

            var result = _mapper.Map<IList<UserVaccineViewModel>>(res.Data);

            return new PagedResponseApi(true, _localizer["UserVaccines"], res.PageNumber, res.PageSize, res.TotalPages, res.TotalRecords, result);
        }

        private async Task GetVaccineDetailsStatus(IList<UserVaccineDetailsDto> detailsDto)
        {
            if (detailsDto.Any())
            {
                var user = await _userDetailsRepository.GetLoadedUserById(detailsDto.FirstOrDefault().UserId);
                var userBirthday = await _userRepository.GetUserBirthdayDate(user.Id);

                foreach (var d in detailsDto)
                    d.Status = user.GetUserVaccineStatus(d.VaccineId, userBirthday);
            }
        }
    }
}
