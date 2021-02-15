using System;

namespace iPassport.Domain.Entities
{
    public abstract class Entity
    {
        protected Entity()
        {
            CreateDate = DateTime.Now;
            UpdateDate = DateTime.Now;
        }

        public Guid Id { get; protected set; }
        public DateTime CreateDate { get; protected set; }
        public DateTime UpdateDate { get; protected set; }


        public void SetUpdateDate() => UpdateDate = DateTime.Now;
    }
}
