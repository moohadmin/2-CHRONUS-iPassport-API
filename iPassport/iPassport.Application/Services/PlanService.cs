using AutoMapper;
using iPassport.Application.Exceptions;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Models.ViewModels;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
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

            var rst = await _repository.InsertAsync(plan);
            if (!rst)
                throw new BusinessException("Não foi possivel Realizar a operação");

            var result = _mapper.Map<PlanViewModel>(plan);

            return new ResponseApi(true, "Plano criado com sucesso!", result);
        }

        public async Task<ResponseApi> GetAll()
        {
            var res = await _repository.FindAll();

            var result = _mapper.Map<IList<PlanViewModel>>(res).OrderBy(r => r.CreateDate).ToList();

            return new ResponseApi(true, "Lista de Planos", result);
        }
    }
}
