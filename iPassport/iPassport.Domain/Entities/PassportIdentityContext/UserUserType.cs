using iPassport.Domain.Entities.Authentication;
using System;

namespace iPassport.Domain.Entities
{
    public class UserUserType 
    {
        public UserUserType() { }

        public UserUserType(Guid userId, Guid userTypeId) : base()
        {
            UserId = userId;
            UserTypeId = userTypeId;            
        }
        public Guid UserId { get; private set; }
        public Guid UserTypeId { get; private set; }
        public DateTime? DeactivationDate { get; private set; }
        public Guid? DeactivationUserId { get; private set; }

        public virtual Users User { get; set; }
        public virtual UserType UserType { get; set; }
        public virtual Users DeactivationUser { get; set; }
    }
}

