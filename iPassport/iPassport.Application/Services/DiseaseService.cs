using AutoMapper;
using iPassport.Application.Interfaces;
using iPassport.Application.Models.Pagination;
using iPassport.Application.Models.ViewModels;
using iPassport.Domain.Filters;
using iPassport.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Application.Services
{
    public class DiseaseService : IDiseaseService
    {
        private readonly IMapper _mapper;
        private readonly IDiseaseRepository _repository;

        public DiseaseService(IMapper mapper, IDiseaseRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<PagedResponseApi> GetByNameInitals(GetByNameInitalsPagedFilter filter)
        {
            var res = await _repository.GetByNameInitals(filter);

            var result = _mapper.Map<IList<DiseaseViewModel>>(res.Data);

            return new PagedResponseApi(true, "Doenças", res.PageNumber, res.PageSize, res.TotalPages, res.TotalRecords, result);
        }
    }
}
