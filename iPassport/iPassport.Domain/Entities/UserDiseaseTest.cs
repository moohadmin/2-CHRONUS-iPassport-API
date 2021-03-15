using System;

namespace iPassport.Domain.Entities
{
    public class UserDiseaseTest : Entity
    {
        public UserDiseaseTest() { Id = Guid.NewGuid(); }
        
        public UserDiseaseTest(Guid diseaseId, Guid userId, bool result, DateTime testDate, DateTime resultDate)
        {
            Id = Guid.NewGuid();
            DiseaseId = diseaseId;
            UserId = userId;
            Result = result;
            TestDate = testDate;
            ResultDate = resultDate;
        }

        public Guid DiseaseId { get; private set; }
        public Guid UserId { get; private set; }
        public bool? Result { get; private set; }
        public DateTime TestDate { get; private set; }
        public DateTime? ResultDate { get; private set; }

        public virtual UserDetails User { get; set; }
        public virtual Disease Disease { get; set; }
    }
}
