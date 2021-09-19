using iPassport.Domain.Entities;
using System;

namespace iPassport.Domain.Dtos
{
    public class ProfileDto
    {
        public ProfileDto(Profile profile)
        {
            Id = profile.Id;
            Name = profile.Name;
            Key = profile.Key;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
    }
}
