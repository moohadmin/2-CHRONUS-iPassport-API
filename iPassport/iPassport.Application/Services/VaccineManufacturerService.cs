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
    public class VaccineManufacturerService : IVaccineManufacturerService
    {
        private readonly IMapper _mapper;
        private readonly IVaccineManufacturerRepository _repository;

        public VaccineManufacturerService(IMapper mapper, IVaccineManufacturerRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<PagedResponseApi> GetByNameInitals(GetByNameInitalsFilter filter)
        {
            var res = await _repository.GetByNameInitals(filter);

            var result = _mapper.Map<IList<VaccineManufacturerViewModel>>(res.Data);

            return new PagedResponseApi(true, "Fabricantes de vacinas", res.PageNumber, res.PageSize, res.TotalPages, res.TotalRecords, result);
        }
    }
}
