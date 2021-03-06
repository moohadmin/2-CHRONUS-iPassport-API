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
    public class StateService : IStateService
    {
        private readonly IStateRepository  _stateRepository;
        private readonly IStringLocalizer<Resource> _localizer;
        private readonly IMapper _mapper;
        
        public StateService(IStateRepository stateRepository, IStringLocalizer<Resource> localizer, IMapper mapper)
        {
            _stateRepository = stateRepository;
            _localizer = localizer;
            _mapper = mapper;
        }

        public async Task<PagedResponseApi> GetByCountryId(GetByIdPagedFilter filter)
        {
            var res = await _stateRepository.GetByCountryId(filter);
            var data = _mapper.Map<IList<StateViewModel>>(res.Data);

            return new PagedResponseApi(true, _localizer["States"], res.PageNumber, res.PageSize, res.TotalPages, res.TotalRecords, data);
        }

        public async Task<ResponseApi> Add(StateCreateDto dto)
        {
            var state = new State().Create(dto);
            var res = await _stateRepository.InsertAsync(state);

            if (!res)
                throw new BusinessException(_localizer["OperationNotPerformed"]);

            return new ResponseApi(true, _localizer["StateCreated"], state.Id);
        }
    }
}
