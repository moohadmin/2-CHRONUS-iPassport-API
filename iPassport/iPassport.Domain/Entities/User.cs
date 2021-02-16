using System;

namespace iPassport.Domain.Entities
{
    public class User : Entity
    {
        public User(string username, string password, string email, string mobile, string profile, string role)
        {
            Id = Guid.NewGuid();
            Username = username;
            Password = password;
            Email = email;
            Mobile = mobile;
            Profile = profile;
            Role = role;
            UserDetails = new UserDetails();
        }

        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }
        public string Mobile { get; private set; }
        public string Profile { get; private set; }
        public string Role { get; private set; }

        public virtual UserDetails UserDetails { get; set; }

        public void ResetPassword(string pwd) => Password = pwd;
    }
}
