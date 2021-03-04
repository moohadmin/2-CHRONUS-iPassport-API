using iPassport.Domain.Dtos;
using System;
using System.Collections.Generic;

namespace iPassport.Domain.Entities
{
    public class Plan : Entity
    {
        public Plan() { }

        public Plan(string type, string description, decimal? price, string obs,  string colorStart, string colorEnd, bool active) : base()
        {
            Id = Guid.NewGuid();
            Type = type;
            Description = description;

            if (price.HasValue)
                Price = price.Value;

            if (!string.IsNullOrEmpty(obs))
                Observation = obs;

            ColorStart = colorStart;
            ColorEnd = colorEnd;
            Active = active;
        }

        public Plan Create(PlanCreateDto dto) => new Plan(dto.Type, dto.Description, dto.Price, dto.Observation, dto.ColorStart, dto.ColorEnd, dto.Active);
        public string Type { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public string Observation { get; private set; }
        public string ColorStart { get; private set; }
        public string ColorEnd { get; private set; }
        public bool Active { get; private set; }

        public virtual IEnumerable<UserDetails> Users { get; set; }

    }
}
