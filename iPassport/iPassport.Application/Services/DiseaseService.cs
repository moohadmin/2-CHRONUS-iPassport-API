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
    public class DiseaseService : IDiseaseService
    {
        private readonly IMapper _mapper;
        private readonly IDiseaseRepository _repository;
        private readonly IStringLocalizer<Resource> _localizer;

        public DiseaseService(IMapper mapper, IDiseaseRepository repository, IStringLocalizer<Resource> localizer)
        {
            _mapper = mapper;
            _repository = repository;
            _localizer = localizer;
        }

        public async Task<PagedResponseApi> GetByNameInitals(GetByNameInitalsFilter filter)
        {
            var res = await _repository.GetByNameInitals(filter);

            var result = _mapper.Map<IList<DiseaseViewModel>>(res.Data);

            return new PagedResponseApi(true, _localizer["Diseases"], res.PageNumber, res.PageSize, res.TotalPages, res.TotalRecords, result);
        }
    }
}
