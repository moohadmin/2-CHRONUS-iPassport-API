using iPassport.Domain.Enums;
using System;
using System.Collections.Generic;

namespace iPassport.Domain.Entities
{
    public class UserType : Entity
    {
        public UserType() { }

        public UserType(string name, int identifyer) : base()
        {
            Id = Guid.NewGuid();
            Name = name;
            Identifyer = identifyer;

        }

        public string Name { get; private set; }
        public int Identifyer { get; private set; }
        public virtual IEnumerable<UserUserType> UserUserTypes { get; set; }

        public bool IsAdmin() => Identifyer == (int)EUserType.Admin;
        public bool IsCitizen() => Identifyer == (int)EUserType.Citizen;
        public bool IsAgent() => Identifyer == (int)EUserType.Agent;
        public bool IsType(EUserType userTypeIdentifyer) => Identifyer == (int)userTypeIdentifyer;

    }
}
