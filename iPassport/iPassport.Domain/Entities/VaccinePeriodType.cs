using System.Collections.Generic;

namespace iPassport.Domain.Entities
{
    public class VaccinePeriodType : Entity
    {
        public string Description { get; set; }
        public int Identifyer { get; private set; }

        public virtual IEnumerable<AgeGroupVaccineDosageType> AgeGroupVaccineDosageTypes { get; set; }
        public virtual IEnumerable<GeneralVaccineDosageType> GeneralVaccineDosageTypes { get; set; }
    }
}
