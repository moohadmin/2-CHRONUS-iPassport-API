using iPassport.Domain.Dtos;
using System;
using System.Collections.Generic;

namespace iPassport.Domain.Entities
{
    public class Plan : Entity
    {
        public Plan() { }

        public Plan(string type, string description, decimal? price = null) : base()
        {
            Id = Guid.NewGuid();
            Type = type;
            Description = description;
            
            if (price.HasValue)
                Price = price.Value;
        }

        public Plan Create(PlanCreateDto dto) => new Plan(dto.Type, dto.Description, dto.Price);
        public string Type { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }

        public virtual IEnumerable<UserDetails> Users { get; set; }

    }
}
