using AutoMapper;
using iPassport.Application.Exceptions;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Models.ViewModels;
using iPassport.Domain.Entities;
using iPassport.Domain.Repositories;
using Microsoft.AspNetCore.Http;

using System.Threading.Tasks;

namespace iPassport.Application.Services
{
    public class PassportService : IPassportService
    {
        private readonly IMapper _mapper;
        private readonly IPassportRepository _repository;
        private readonly IUserDetailsRepository _userDetailsRepository;
        private readonly IHttpContextAccessor _accessor;

        public PassportService(IMapper mapper, IPassportRepository repository, IUserDetailsRepository userDetailsRepository, IHttpContextAccessor accessor)
        {
            _mapper = mapper;
            _repository = repository;
            _userDetailsRepository = userDetailsRepository;
            _accessor = accessor;
        }
        public async Task<ResponseApi> Get(string userId)
        {
            System.Guid UserId;
            if (!System.Guid.TryParse(userId, out UserId))
                throw new BusinessException("Usuário não encontrado.");

            var userDetails = await _userDetailsRepository.FindWithUser(UserId);

            if (userDetails == null)
                throw new BusinessException("Usuário não encontrado.");

            var passport = await _repository.FindByUser(userDetails.UserId);

            if (passport == null)
            {
                passport = new Passport();
                passport = passport.Create(userDetails);

                await _repository.InsertAsync(passport);
            }

            return new ResponseApi(true, "Passport do Usuário", _mapper.Map<PassportViewModel>(passport));
        }
    }
}
