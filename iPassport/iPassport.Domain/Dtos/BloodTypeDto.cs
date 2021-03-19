using iPassport.Domain.Entities;
using System;

namespace iPassport.Domain.Dtos
{
    public class BloodTypeDto
    {
        public BloodTypeDto(BloodType bBloodType)
        {
            Id = bBloodType?.Id;
            Name = bBloodType?.Name;
        }

        public Guid? Id { get; set; }
        public string Name { get; set; }
    }
}