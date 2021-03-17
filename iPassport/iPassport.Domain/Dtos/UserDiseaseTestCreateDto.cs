using System;

namespace iPassport.Domain.Dtos
{
    public class UserDiseaseTestCreateDto
    {
        public Guid DiseaseId { get;  set; }
        public Guid UserId { get;  set; }
        public bool? Result { get;  set; }
        public DateTime TestDate { get;  set; }
        public DateTime? ResultDate { get;  set; }
    }
}
