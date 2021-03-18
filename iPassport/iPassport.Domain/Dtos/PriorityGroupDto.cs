using iPassport.Domain.Entities;
using System;

namespace iPassport.Domain.Dtos
{
    public class PriorityGroupDto
    {
        public PriorityGroupDto(PriorityGroup pPriorityGroup)
        {
            Id = pPriorityGroup?.Id;
            Name = pPriorityGroup?.Name;
        }

        public Guid? Id { get; set; }
        public string Name { get; set; }
    }
}