using iPassport.Domain.Utils;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;


namespace iPassport.Test.Settings.Factories
{
    public static class HttpContextAccessorFactory
    {

        public static IHttpContextAccessor Create()
        {
            var httpContext = new DefaultHttpContext();
            var httpContextAccessor = new HttpContextAccessor();

            httpContextAccessor.HttpContext = httpContext;
            httpContextAccessor.HttpContext.User = new ClaimsPrincipal(new List<ClaimsIdentity>() { new ClaimsIdentity(new List<Claim>() { 
                new Claim(Constants.TOKEN_CLAIM_USER_ID, Guid.NewGuid().ToString())
                ,new Claim(ClaimTypes.Role, "admin")
                ,new Claim(Constants.TOKEN_CLAIM_CITY_ID, Guid.NewGuid().ToString())
                ,new Claim(Constants.TOKEN_CLAIM_STATE_ID, Guid.NewGuid().ToString())
                ,new Claim(Constants.TOKEN_CLAIM_COUNTRY_ID, Guid.NewGuid().ToString())
                ,new Claim(Constants.TOKEN_CLAIM_COMPANY_ID, Guid.NewGuid().ToString())
                ,new Claim(Constants.TOKEN_CLAIM_HEALTH_UNITY_ID, Guid.NewGuid().ToString())
            }) });


            return httpContextAccessor;
        }
    }
}
