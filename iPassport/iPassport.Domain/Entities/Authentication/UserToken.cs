using Microsoft.AspNetCore.Identity;
using System;

namespace iPassport.Domain.Entities.Authentication
{
    public class UserToken
    {
        public UserToken() { }

        public UserToken(string provider, Guid userId, string token)
        {
            IsActive = true;
            UserId = userId;
            Provider = provider;
            Value = token;
        }
        public virtual Guid UserId { get; set; }
        public bool IsActive { get; private set; }
        public virtual string Provider { get; set; }
        public virtual string Value { get; set; }
        
        public void Deactivate() => IsActive = false;
    }
}
