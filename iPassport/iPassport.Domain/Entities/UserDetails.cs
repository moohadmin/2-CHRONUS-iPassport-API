using System;

namespace iPassport.Domain.Entities
{
    public class UserDetails : Entity
    {
        public UserDetails() { }

        public UserDetails(Guid userId, string fullName, int cpf, int cns, DateTime birthday, string gender, string breed, string bloodType, string occupation, string address, string photo)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            FullName = fullName;
            CPF = cpf;
            CNS = cns;
            Birthday = birthday;
            Gender = gender;
            Breed = breed;
            BloodType = bloodType;
            Occupation = occupation;
            Address = address;
            Photo = photo;
        }

        public Guid UserId { get; private set; }
        public string FullName { get; private set; }
        public int CPF { get; private set; }
        public int CNS { get; private set; }
        public DateTime Birthday { get; private set; }
        public string Gender { get; private set; }
        public string Breed { get; private set; }
        public string BloodType { get; private set; }
        public string Occupation { get; private set; }
        public string Address { get; private set; }
        public string Photo { get; private set; }

        public virtual User User { get; set; }
    }
}
