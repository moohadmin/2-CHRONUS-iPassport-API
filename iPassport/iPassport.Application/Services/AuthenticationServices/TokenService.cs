using iPassport.Application.Exceptions;
using iPassport.Application.Extensions;
using iPassport.Application.Interfaces.Authentication;
using iPassport.Application.Resources;
using iPassport.Domain.Entities.Authentication;
using iPassport.Domain.Repositories.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace iPassport.Application.Services.AuthenticationServices
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _accessor;
        private readonly IUserTokenRepository _userTokenRepository;
        private readonly IStringLocalizer<Resource> _localizer;

        public TokenService(IConfiguration configuration,
            IHttpContextAccessor accessor,
            IUserTokenRepository userTokenRepository,
            IStringLocalizer<Resource> localizer)
        {
            _configuration = configuration;
            _accessor = accessor;
            _userTokenRepository = userTokenRepository;
            _localizer = localizer;
        } 

        public async Task<string> GenerateBasic(Users user, bool hasPlan)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("Secret").Value);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", user.Id.ToString()),
                    new Claim("Profile", user.Profile.ToString()),
                    new Claim("HasPhoto", user.UserHavePhoto().ToString()),
                    new Claim("HasPlan", hasPlan.ToString())
                }),
                Expires = DateTime.UtcNow.AddYears(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);
            
            await AddUserTokenAsync(jwt, user.Id);
            
            return jwt;
        }

        public async Task<string> GenerateByEmail(Users user, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("Secret").Value);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", user.Id.ToString()),
                    new Claim("FirstLogin", (user.LastLogin == null).ToString()),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);
            
            await AddUserTokenAsync(jwt, user.Id);

            return jwt;
        }

        public async Task<bool> IsCurrentActiveToken() =>
            await IsActiveAsync(_accessor.GetCurrentToken());

        public async Task DeactivateCurrentAsync() =>
            await DeactivateAsync(_accessor.GetCurrentToken());

        public async Task DeactivateAsync(string token)
        {
            var userTkn = await GetUserTokenAsync(token);

            if (userTkn == null)
                throw new BusinessException(_localizer["OperationNotPerformed"]);

            userTkn.Deactivate();

            var res = await _userTokenRepository.Update(userTkn);
            
            if (!res)
                throw new BusinessException(_localizer["OperationNotPerformed"]);
        }

        public async Task<bool> IsActiveAsync(string token)
        {
            var userTkn = await GetUserTokenAsync(token);

            return userTkn != null ? userTkn.IsActive : false;
        }

        private async Task<UserToken> GetUserTokenAsync(string token)
        {
            var userTkn = await _userTokenRepository.GetByToken(token);
                        
            return userTkn;
        }

        private async Task AddUserTokenAsync(string token, Guid userId)
        {
            var activeTkn = await _userTokenRepository.GetActive(userId);

            if (activeTkn != null)
                await DeactivateAsync(activeTkn);
            
            var userTkn = new UserToken("ipassport", userId, token);

            var res = await _userTokenRepository.InsertAsync(userTkn);

            if (!res)
                throw new BusinessException(_localizer["OperationNotPerformed"]);
        }

        private async Task DeactivateAsync(UserToken userTkn)
        {
            userTkn.Deactivate();

            var res = await _userTokenRepository.Update(userTkn);

            if (!res)
                throw new BusinessException(_localizer["OperationNotPerformed"]);
        }
    }
}
