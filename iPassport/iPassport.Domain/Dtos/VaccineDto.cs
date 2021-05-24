using iPassport.Domain.Enums;
using System;
using System.Collections.Generic;

namespace iPassport.Domain.Dtos
{
    public class VaccineDto
    {
        public Guid? Id {get;set;}
        public string Name { get; set; }
        public Guid Manufacturer { get; set; }
        public IEnumerable<Guid> Diseases { get; set; }
        public int ExpirationTimeInMonths { get; set; }
        public int ImmunizationTimeInDays { get; set; }
        public bool IsActive { get; set; }
        public EVaccineDosageType DosageType { get; set; }
        public Guid DosageTypeId { get; set; }
        public GeneralGroupVaccineDto GeneralGroupVaccine { get; set; }
        public IEnumerable<AgeGroupVaccineDto> AgeGroupVaccines { get; set; }
    }
}
