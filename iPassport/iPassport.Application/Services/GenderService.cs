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
    public class GenderService : IGenderService
    {
        private readonly IGenderRepository _genderRepository;        
        private readonly IStringLocalizer<Resource> _localizer;
        private readonly IMapper _mapper;

        public GenderService(IGenderRepository genderRepository, IStringLocalizer<Resource> localizer, IMapper mapper)
        {
            _genderRepository = genderRepository;
            _localizer = localizer;
            _mapper = mapper;
        }

        public async Task<PagedResponseApi> FindByNameParts(GetByNamePartsPagedFilter filter)
        {
            var res = await _genderRepository.FindByNameParts(filter);

            var result = _mapper.Map<IList<GenderViewModel>>(res.Data);

            return new PagedResponseApi(true, _localizer["Genders"], res.PageNumber, res.PageSize, res.TotalPages, res.TotalRecords, result);

        }
    }
}
