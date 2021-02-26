using iPassport.Application.Exceptions;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Domain.Entities.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace iPassport.Application.Services.AuthenticationServices
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<Roles> _roleManager;

        public RoleService(RoleManager<Roles> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<ResponseApi> Add(string urn)
        {
            var roles = new Roles
            {
                Id = Guid.NewGuid(),
                Name = urn
            };

            var _role = await _roleManager.CreateAsync(roles);
            if(!_role.Succeeded)
                throw new BusinessException("Houve um erro, tente novamente!");

            return new ResponseApi(_role.Succeeded, "Role criado com sucesso!", roles.Id);
        }
    }
}
