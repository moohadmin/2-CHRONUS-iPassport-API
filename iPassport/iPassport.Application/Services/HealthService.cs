using AutoMapper;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Models.ViewModels;
using iPassport.Domain.Entities;
using iPassport.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iPassport.Application.Services
{
    public class HealthService : IHealthService
    {
        private readonly IMapper _mapper;
        private readonly IHealthRepository _repository;

        public HealthService(IMapper mapper, IHealthRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ResponseApi> SetHealthyAsync()
        {
            var health = new Health();
            health.Healthy();

            await _repository.InsertAsync(health);

            return new ResponseApi(true, "iPassport Api is Healthy!", health.Id);
        }

        public async Task<ResponseApi> GetAll()
        {
            var res = await _repository.FindAll();

            var result = _mapper.Map<IList<HealthViewModel>>(res);

            return new ResponseApi(true, "List of HealthChecks Test!", result);
        }
    } 
}
