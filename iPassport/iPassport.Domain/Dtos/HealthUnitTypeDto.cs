using iPassport.Domain.Entities;
using System;

namespace iPassport.Domain.Dtos
{
    public class HealthUnitTypeDto
    {
        public HealthUnitTypeDto(HealthUnitType type)
        {
            Id = type.Id;
            Name = type.Name;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}