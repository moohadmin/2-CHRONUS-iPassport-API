using AutoMapper;
using iPassport.Application.Exceptions;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Models.Pagination;
using iPassport.Application.Models.ViewModels;
using iPassport.Application.Resources;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories.PassportIdentityContext;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Application.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository  _cityRepository;
        private readonly IStringLocalizer<Resource> _localizer;
        private readonly IMapper _mapper;
        
        public CityService(ICityRepository cityRepository, IStringLocalizer<Resource> localizer, IMapper mapper)
        {
            _cityRepository = cityRepository;
            _localizer = localizer;
            _mapper = mapper;
        }

        public async Task<PagedResponseApi> FindByStateAndNameParts(GetByIdAndNamePartsPagedFilter filter)
        {
            var res = await _cityRepository.FindByStateAndNameParts(filter);
            var data = _mapper.Map<IList<CityViewModel>>(res.Data);

            return new PagedResponseApi(true, _localizer["Cities"], res.PageNumber, res.PageSize, res.TotalPages, res.TotalRecords, data);
        }

        //public async Task<ResponseApi> Add(StateCreateDto dto)
        //{
        //    var state = new State().Create(dto);
        //    var res = await _stateRepository.InsertAsync(state);

        //    if (!res)
        //        throw new BusinessException(_localizer["OperationNotPerformed"]);

        //    return new ResponseApi(true, _localizer["StateCreated"], state.Id);
        //}
    }
}
