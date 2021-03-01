using iPassport.Domain.Dtos;
using System;
using System.Collections.Generic;

namespace iPassport.Domain.Entities
{
    public class Vaccine : Entity
    {
        public Vaccine() { }

        public Vaccine(string name, Guid manufacturerId, int requiredDoses, int expirationTime, int imunizationTime, bool uniqueDose = false) : base()
        {
            Id = Guid.NewGuid();
            Name = name;
            ManufacturerId = manufacturerId;
            ExpirationTime = expirationTime;
            ImunizationTime = imunizationTime;
            UniqueDose = uniqueDose;
            
            if(!uniqueDose)
                RequiredDoses = requiredDoses;
            else
                RequiredDoses = 1;

        }
        public string Name { get; private set; }
        public int RequiredDoses { get; private set; }
        public int ExpirationTime { get; private set; }
        public int ImunizationTime { get; private set; }
        public Guid ManufacturerId { get; private set; }
        public bool UniqueDose { get; private set; }
        public virtual VaccineManufacturer Manufacturer { get; set; }
        public virtual IEnumerable<Disease> Diseases { get; set; }
        public virtual IEnumerable<UserVaccine> UserVaccines { get; set; }

        public Vaccine Create(VaccineCreateDto dto) => new Vaccine(dto.Name, dto.ManufacturerId, dto.RequiredDoses, dto.ExpirationTime, dto.ImunizationTime, dto.UniqueDose);
    }
}
