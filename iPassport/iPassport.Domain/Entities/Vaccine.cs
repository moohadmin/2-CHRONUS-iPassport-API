using iPassport.Domain.Dtos;
using System;
using System.Collections.Generic;

namespace iPassport.Domain.Entities
{
    public class Vaccine : Entity
    {
        public Vaccine() { }

        public Vaccine(string description, string laboratory, int requiredDoses, int expirationTime, int imunizationTime) : base()
        {
            Id = Guid.NewGuid();
            Description = description;
            Laboratory = laboratory;
            RequiredDoses = requiredDoses;
            ExpirationTime = expirationTime;
            ImunizationTime = imunizationTime;
        }

        public string Description { get; private set; }
        public string Laboratory { get; private set; }
        public int RequiredDoses { get; private set; }
        public int ExpirationTime { get; private set; }
        public int ImunizationTime { get; private set; }

        public virtual IEnumerable<Disease> Diseases { get; set; }
        public virtual IEnumerable<UserVaccine> UserVaccines { get; set; }

        public Vaccine Create(VaccineCreateDto dto) => new Vaccine(dto.Description, dto.Laboratory, dto.RequiredDoses, dto.ExpirationTime, dto.ImunizationTime);
    }
}
