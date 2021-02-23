using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace iPassport.Test.Settings.Factories
{
    public static class ControllerContextFactory
    {
        public static ControllerContext Create()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                { new Claim(("UserId"), "7d4bb42-a0cb-4a72-abaa-ded84823a166")}, "mock"));

            return new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
        }
    }
}
