using System.Collections.Generic;

namespace iPassport.Domain.Entities
{
    public class VaccinePeriodType : Entity
    {
        public string Description { get; private set; }
        public int Identifyer { get; private set; }

        public virtual IEnumerable<AgeGroupVaccine> AgeGroupVaccines { get; set; }
        public virtual IEnumerable<GeneralGroupVaccine> GeneralGroupVaccine { get; set; }
    }
}
