using iPassport.Domain.Entities;
using System;

namespace iPassport.Domain.Dtos
{
    public class HumanRaceDto
    {
        public HumanRaceDto(HumanRace humanRace)
        {
            Id = humanRace?.Id;
            Name = humanRace?.Name;
        }

        public Guid? Id { get; set; }
        public string Name { get; set; }
    }
}