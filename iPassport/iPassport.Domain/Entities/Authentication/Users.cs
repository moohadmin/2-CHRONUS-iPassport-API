using Microsoft.AspNetCore.Identity;
using System;

namespace iPassport.Domain.Entities.Authentication
{
    public class Users : IdentityUser<Guid>
    {
        public bool AcceptTerms { get; set; }
        public DateTime UpdateDate { get; set; }

        public void SetAcceptTerms(bool acceptTerms) => AcceptTerms = acceptTerms;
        public void SetUpdateDate() => UpdateDate = DateTime.Now;
    }
}
