using iPassport.Domain.Dtos;
using System;
using System.Collections.Generic;

namespace iPassport.Domain.Entities
{
    public class Vaccine : Entity
    {
        public Vaccine() { }

        public Vaccine(string name, Guid manufacturerId, int requiredDoses, int expirationTime, int imunizationTime,int maxTimeNextDose, int minTimeNextDose) : base()
        {
            Id = Guid.NewGuid();
            Name = name;
            ManufacturerId = manufacturerId;
            ExpirationTimeInMonths = expirationTime;
            ImmunizationTimeInDays = imunizationTime;
            RequiredDoses = requiredDoses;            
            MaxTimeNextDose = maxTimeNextDose;
            MinTimeNextDose = minTimeNextDose;
        }
        public string Name { get; private set; }
        public int RequiredDoses { get; private set; }
        public int ExpirationTimeInMonths { get; private set; }
        public int ImmunizationTimeInDays { get; private set; }
        public Guid ManufacturerId { get; private set; }
        
        public int MaxTimeNextDose { get; private set; }
        public int MinTimeNextDose { get; private set; }

        public virtual VaccineManufacturer Manufacturer { get; set; }
        public virtual IEnumerable<Disease> Diseases { get; set; }
        public virtual IEnumerable<UserVaccine> UserVaccines { get; set; }

        public Vaccine Create(VaccineCreateDto dto) => new Vaccine(dto.Name, dto.ManufacturerId, dto.RequiredDoses, dto.ExpirationTime, dto.ImunizationTime, dto.MaxTimeNextDose, dto.MinTimeNextDose);

        public bool UniqueDose() => RequiredDoses == 1;
    }
}
