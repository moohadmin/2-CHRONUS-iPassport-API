using AutoMapper;
using iPassport.Application.Interfaces;
using iPassport.Application.Models.Pagination;
using iPassport.Application.Models.ViewModels;
using iPassport.Application.Resources;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories.PassportIdentityContext;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Application.Services
{
    public class HumanRaceService : IHumanRaceService
    {
        private readonly IHumanRaceRepository _humanRaceRepository;        
        private readonly IStringLocalizer<Resource> _localizer;
        private readonly IMapper _mapper;

        public HumanRaceService(IHumanRaceRepository humanRaceRepository, IStringLocalizer<Resource> localizer, IMapper mapper)
        {
            _humanRaceRepository = humanRaceRepository;
            _localizer = localizer;
            _mapper = mapper;
        }

        public async Task<PagedResponseApi> FindByNameParts(GetByNamePartsPagedFilter filter)
        {
            var res = await _humanRaceRepository.FindByNameParts(filter);

            var result = _mapper.Map<IList<HumanRaceViewModel>>(res.Data);

            return new PagedResponseApi(true, _localizer["HumanRaces"], res.PageNumber, res.PageSize, res.TotalPages, res.TotalRecords, result);

        }
    }
}
