using iPassport.Domain.Entities;
using System;

namespace iPassport.Domain.Dtos
{
    public class GenderDto
    {
        public GenderDto(Gender gGender)
        {
            Id = gGender?.Id;
            Name = gGender?.Name;
        }

        public Guid? Id { get; set; }
        public string Name { get; set; }
    }
}