using iPassport.Domain.Entities;
using System;

namespace iPassport.Domain.Dtos
{
    public class UserDiseaseTestDto
    {
        public UserDiseaseTestDto(UserDiseaseTest userDiseaseTest)
        {
            Id = userDiseaseTest?.Id;
            Name = userDiseaseTest?.Name;
            Result = userDiseaseTest?.Result;
            TestDate = userDiseaseTest?.TestDate;
            ResultDate = userDiseaseTest?.ResultDate;
        }

        public string Name { get; set; }
        public Guid? Id { get; set; }
        public bool? Result { get; set; }
        public DateTime? TestDate { get; set; }
        public DateTime? ResultDate { get; set; }
    }
}