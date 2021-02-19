using AutoMapper;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Models.ViewModels;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
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

        public async Task<ResponseApi> Add(PlanCreateDto dto)
        {
            var plan = new Plan();
            plan = plan.Create(dto);

            await _repository.InsertAsync(plan);

            var result = _mapper.Map<PlanViewModel>(plan);

            return new ResponseApi(true, "Plano criado com sucesso!", result);
        }

        public async Task<ResponseApi> GetAll()
        {
            var res = await _repository.FindAll();

            var result = _mapper.Map<IList<PlanViewModel>>(res);

            return new ResponseApi(true, "Lista de Planos", result);
        }
    }
}
