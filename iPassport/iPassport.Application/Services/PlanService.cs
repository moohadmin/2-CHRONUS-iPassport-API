using AutoMapper;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Models.ViewModels;
using iPassport.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Application.Services
{
    public class PlanService : IPlanService
    {
        private readonly IMapper _mapper;
        private readonly IPlanRepository _repository;

        public PlanService(IMapper mapper, IPlanRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ResponseApi> GetAll()
        {
            var res = await _repository.FindAll();

            var result = _mapper.Map<IList<PlanViewModel>>(res);

            return new ResponseApi(true, "List of Plans", result);
        }
    }
}
