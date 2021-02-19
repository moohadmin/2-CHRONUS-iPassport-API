using iPassport.Domain.Dtos;

namespace iPassport.Domain.Entities
{
    public class User : Entity
    {
        public User() { }

        public User(string username, string password, bool passwordIsValid, string email, string mobile, string profile, string role)
        {
            Id = System.Guid.NewGuid();
            Username = username;
            Password = password;
            PasswordIsValid = passwordIsValid;
            Email = email;
            Mobile = mobile;
            Profile = profile;
            Role = role;
            UserDetails = new UserDetails();
        }

        public string Username { get; private set; }
        public string Password { get; private set; }
        public bool PasswordIsValid { get; private set; }
        public string Email { get; private set; }
        public string Mobile { get; private set; }
        public string Profile { get; private set; }
        public string Role { get; private set; }

        public virtual UserDetails UserDetails { get; set; }


        public void ResetPassword(string pwd) => Password = pwd;

        public User Create(UserCreateDto dto) => new User(dto.Username, dto.Password, dto.PasswordIsValid, dto.Email, dto.Mobile, dto.Profile, dto.Role);
    }
}
