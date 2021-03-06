using Microsoft.AspNetCore.Identity;
using System;

namespace iPassport.Domain.Entities.Authentication
{
    public class UserToken : IdentityUserToken<Guid>
    {
        public UserToken() { }

        public UserToken(string loginProvider, string name, Guid userId, string token)
        {
            IsActive = true;
            UserId = userId;
            LoginProvider = loginProvider;
            Value = token;
            Name = name;
        }

        public bool IsActive { get; private set; }

        public void Deactivate() => IsActive = false;
    }
}
