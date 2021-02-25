using iPassport.Domain.Dtos;
using System;
using System.Collections.Generic;

namespace iPassport.Domain.Entities
{
    public class Plan : Entity
    {
        public Plan() { }

        public Plan(string type, string description, decimal? price = null, string obs = null) : base()
        {
            Id = Guid.NewGuid();
            Type = type;
            Description = description;

            if (price.HasValue)
                Price = price.Value;

            if (!string.IsNullOrEmpty(obs))
                Observation = obs;
        }

        public Plan Create(PlanCreateDto dto) => new Plan(dto.Type, dto.Description, dto.Price, dto.Observation);
        public string Type { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public string Observation { get; set; }

        public virtual IEnumerable<UserDetails> Users { get; set; }

    }
}
