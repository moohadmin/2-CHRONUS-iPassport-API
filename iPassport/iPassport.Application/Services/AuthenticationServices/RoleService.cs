using iPassport.Application.Exceptions;
using iPassport.Application.Interfaces;
using iPassport.Application.Models;
using iPassport.Application.Resources;
using iPassport.Domain.Entities.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System;
using System.Threading.Tasks;

namespace iPassport.Application.Services.AuthenticationServices
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<Roles> _roleManager;
        private readonly IStringLocalizer<Resource> _localizer;

        public RoleService(RoleManager<Roles> roleManager, IStringLocalizer<Resource> localizer)
        {
            _roleManager = roleManager;
            _localizer = localizer;
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
                throw new BusinessException(_localizer["GenericError"]);

            return new ResponseApi(_role.Succeeded, _localizer["RoleCreated"], roles.Id);
        }
    }
}
