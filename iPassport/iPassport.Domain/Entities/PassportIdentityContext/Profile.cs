using iPassport.Domain.Enums;
using System;

namespace iPassport.Domain.Entities
{
    public class Profile : Entity
    {
        public Profile() { }
        public Profile(string name, string key)
        {
            Id = System.Guid.NewGuid();
            Name = name;
            Key = key;
        }
        public string Name { get; private set; }
        public string Key { get; private set; }

        public bool IsBusiness() => Key == Enum.GetName(EProfileKey.business);
        public bool IsGovernment() => Key == Enum.GetName(EProfileKey.government);
        public bool IsHealthUnit() => Key == Enum.GetName(EProfileKey.healthUnit);
        public bool IsAdmin() => Key == Enum.GetName(EProfileKey.admin);
    }
}
