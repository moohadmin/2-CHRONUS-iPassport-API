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
            httpContextAccessor.HttpContext.User = new ClaimsPrincipal(new List<ClaimsIdentity>() { new ClaimsIdentity(new List<Claim>() { new Claim("UserId", Guid.NewGuid().ToString()) }) });

            return httpContextAccessor;
        }
    }
}
