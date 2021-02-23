using AutoMapper;
using iPassport.Application.Exceptions;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Models.ViewModels;
using iPassport.Domain.Dtos;
using iPassport.Domain.Entities;
using iPassport.Domain.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace iPassport.Application.Services
{
    public class PassportService : IPassportService
    {
        private readonly IMapper _mapper;
        private readonly IPassportRepository _repository;
        private readonly IPassportUseRepository _useRepository;
        private readonly IUserDetailsRepository _userDetailsRepository;

        public PassportService(IMapper mapper, IPassportRepository repository, IUserDetailsRepository userDetailsRepository, IPassportUseRepository useRepository )
        {
            _mapper = mapper;
            _repository = repository;
            _userDetailsRepository = userDetailsRepository;
            _useRepository = useRepository;
        }

        public async Task<ResponseApi> Get(string userId)
        {
            Guid UserId;
            if (!Guid.TryParse(userId, out UserId))
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
            else
            {
                if (passport.IsAllDetailsExpired())
                {
                    passport.AddNewPassportDetails(null);
                    _repository.Update(passport);
                }
            }

            return new ResponseApi(true, "Passport do Usuário", _mapper.Map<PassportViewModel>(passport));
        }
        public async Task<ResponseApi> AddAccessApproved(PassportUseCreateDto dto)
        {
            dto = await ValidPassportToAcess(dto);
            dto.AllowAccess = true;

            var passportUse = new PassportUse();
            passportUse = passportUse.Create(dto);

            await _useRepository.InsertAsync(passportUse);

            return new ResponseApi(true, "Acesso Aprovado", "Ok");
        }
        public async Task<ResponseApi> AddAccessDenied(PassportUseCreateDto dto)
        {
            dto = await ValidPassportToAcess(dto);
            dto.AllowAccess = false;

            var passportUse = new PassportUse();
            passportUse = passportUse.Create(dto);

            await _useRepository.InsertAsync(passportUse);

            return new ResponseApi(true, "Acesso Recusado", "Ok");
        }

        private async Task<PassportUseCreateDto> ValidPassportToAcess(PassportUseCreateDto dto)
        {
            var passport = await _repository.FindByPassportDetails(dto.PassportDetailsId);

            if (passport == null)
                throw new BusinessException("Passport Não Encontrado");

            if (passport.ListPassportDetails.First(x => x.Id == dto.PassportDetailsId).IsExpired())
                throw new BusinessException("Passport Expirado");

            dto.CitizenId = passport.UserDetailsId;

            return dto;
        }
    }
}
