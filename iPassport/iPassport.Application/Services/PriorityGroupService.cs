using AutoMapper;
using iPassport.Application.Interfaces;
using iPassport.Application.Models.Pagination;
using iPassport.Application.Models.ViewModels;
using iPassport.Application.Resources;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Application.Services
{
    public class PriorityGroupService : IPriorityGroupService
    {
        private readonly IPriorityGroupRepository _priorityGroupRepository;        
        private readonly IStringLocalizer<Resource> _localizer;
        private readonly IMapper _mapper;

        public PriorityGroupService(IPriorityGroupRepository priorityGroupRepository, IStringLocalizer<Resource> localizer, IMapper mapper)
        {
            _priorityGroupRepository = priorityGroupRepository;
            _localizer = localizer;
            _mapper = mapper;
        }

        public async Task<PagedResponseApi> FindByNameParts(GetByNamePartsPagedFilter filter)
        {
            var res = await _priorityGroupRepository.FindByNameParts(filter);

            var result = _mapper.Map<IList<PriorityGroupViewModel>>(res.Data);

            return new PagedResponseApi(true, _localizer["PriorityGroups"], res.PageNumber, res.PageSize, res.TotalPages, res.TotalRecords, result);

        }
    }
}
