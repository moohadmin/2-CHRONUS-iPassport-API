using iPassport.Domain.Dtos;
using iPassport.Domain.Utils;
using System;

namespace iPassport.Domain.Entities
{
    public class UserDiseaseTest : Entity
    {
        public UserDiseaseTest() { Id = Guid.NewGuid(); }
        
        public UserDiseaseTest(Guid userId, bool? result, DateTime testDate, DateTime? resultDate, string name)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Result = result;
            TestDate = testDate;
            ResultDate = resultDate;
            Name = string.IsNullOrWhiteSpace(name) ? Constants.DISEASE_TEST_NAME : name;
        }

        public string Name { get; private set; }
        public Guid UserId { get; private set; }
        public bool? Result { get; private set; }        
        public DateTime TestDate { get; private set; }
        public DateTime? ResultDate { get; private set; }

        public virtual UserDetails User { get; set; }
        

        public UserDiseaseTest Create(UserDiseaseTestCreateDto dto) => new UserDiseaseTest(dto.UserId, dto.Result, dto.TestDate, dto.ResultDate, dto.Name);
    }
}
