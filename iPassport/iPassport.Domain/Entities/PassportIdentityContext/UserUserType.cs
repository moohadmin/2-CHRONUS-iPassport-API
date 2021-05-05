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
            CreateDate = DateTime.UtcNow;
            UpdateDate = DateTime.UtcNow;
        }
        public Guid UserId { get; private set; }
        public Guid UserTypeId { get; private set; }
        public DateTime? DeactivationDate { get; private set; }
        public Guid? DeactivationUserId { get; private set; }
        public DateTime? LastLogin { get; private set; }
        public DateTime? CreateDate { get; private set; }
        public DateTime? UpdateDate { get; private set; }

        public virtual Users User { get; set; }
        public virtual UserType UserType { get; set; }
        public virtual Users DeactivationUser { get; set; }

        public bool IsActive() => !DeactivationDate.HasValue;
        public bool IsInactive() => DeactivationDate.HasValue;

        public void Deactivate(Guid deactivationUserId)
        {
            DeactivationUserId = deactivationUserId;
            DeactivationDate = DateTime.UtcNow;
            UpdateDate = DateTime.UtcNow;
        }
        public void Activate()
        {
            DeactivationUserId = null;
            DeactivationDate = null;
            UpdateDate = DateTime.UtcNow;
        }

        public void UpdateLastLogin()
        {
            LastLogin = DateTime.UtcNow;            
        }
            
    }
}

