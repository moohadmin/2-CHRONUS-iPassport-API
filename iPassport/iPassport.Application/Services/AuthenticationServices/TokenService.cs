using iPassport.Application.Exceptions;
using iPassport.Application.Extensions;
using iPassport.Application.Interfaces.Authentication;
using iPassport.Application.Resources;
using iPassport.Application.Services.Constants;
using iPassport.Domain.Entities.Authentication;
using iPassport.Domain.Repositories.Authentication;
using iPassport.Domain.Utils;
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
        
        private readonly IHttpContextAccessor _accessor;
        private readonly IUserTokenRepository _userTokenRepository;
        private readonly IStringLocalizer<Resource> _localizer;

        public TokenService(IHttpContextAccessor accessor,
            IUserTokenRepository userTokenRepository,
            IStringLocalizer<Resource> localizer)
        {
            _accessor = accessor;
            _userTokenRepository = userTokenRepository;
            _localizer = localizer;
        } 

        public async Task<string> GenerateBasic(Users user, bool hasPlan, string identifyUserType)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(EnvConstants.SECRET_JWT_TOKEN);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(Domain.Utils.Constants.TOKEN_CLAIM_USER_ID, user.Id.ToString()),
                    new Claim(Domain.Utils.Constants.TOKEN_CLAIM_PROFILE, identifyUserType ?? string.Empty),
                    new Claim(Domain.Utils.Constants.TOKEN_CLAIM_HAS_PHOTO, user.UserHavePhoto().ToString()),
                    new Claim(Domain.Utils.Constants.TOKEN_CLAIM_HAS_PLAN, hasPlan.ToString())
                }),
                Expires = DateTime.UtcNow.AddYears(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);
            
            await AddUserTokenAsync(jwt, user.Id);
            
            return jwt;
        }

        public async Task<string> GenerateByEmail(Users user, string companyId, string cityId, string stateId, string countryId, string healthUnityId, string isFirstLoginText, string identifyUserType)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(EnvConstants.SECRET_JWT_TOKEN);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(Domain.Utils.Constants.TOKEN_CLAIM_USER_ID, user.Id.ToString()),
                    new Claim(Domain.Utils.Constants.TOKEN_CLAIM_FIRST_LOGIN, isFirstLoginText ?? string.Empty),
                    new Claim(Domain.Utils.Constants.TOKEN_CLAIM_FULL_NAME, user.FullName),
                    new Claim(ClaimTypes.Role, user.Profile?.Key),
                    new Claim(Domain.Utils.Constants.TOKEN_CLAIM_COMPANY_ID, companyId ?? string.Empty),
                    new Claim(Domain.Utils.Constants.TOKEN_CLAIM_CITY_ID, cityId ?? string.Empty),
                    new Claim(Domain.Utils.Constants.TOKEN_CLAIM_STATE_ID, stateId ?? string.Empty),
                    new Claim(Domain.Utils.Constants.TOKEN_CLAIM_COUNTRY_ID, countryId ?? string.Empty),
                    new Claim(Domain.Utils.Constants.TOKEN_CLAIM_HEALTH_UNITY_ID, healthUnityId ?? string.Empty),
                    new Claim(Domain.Utils.Constants.TOKEN_CLAIM_PROFILE, identifyUserType ?? string.Empty)
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
