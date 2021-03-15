using System;

namespace iPassport.Api.Models.Requests
{
    /// <summary>
    /// Create User Request
    /// </summary>
    public class UserCreateRequest
    {
        /// <summary>
        /// Username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Password Is Valid ?
        /// </summary>
        public bool PasswordIsValid { get; set; }

        /// <summary>
        /// E-mail
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Mobile
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// Profile
        /// </summary>
        public int Profile { get; set; }

        /// <summary>
        /// Full Name
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Cpf
        /// </summary>
        public string CPF { get; set; }

        /// <summary>
        /// Rg
        /// </summary>
        public string RG { get; set; }

        /// <summary>
        /// CNS
        /// </summary>
        public string CNS { get; set; }

        /// <summary>
        /// Passport Document
        /// </summary>
        public string Passport { get; set; }

        /// <summary>
        /// Birthday
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// Gender
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Breed
        /// </summary>
        public string Breed { get; set; }

        /// <summary>
        /// Blood Type
        /// </summary>
        public string BloodType { get; set; }

        /// <summary>
        /// Occupation
        /// </summary>
        public string Occupation { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Photo
        /// </summary>
        public string Photo { get; set; }

        /// <summary>
        /// International Document Code
        /// </summary>
        public string InternationalDocument { get; set; }
    }
}
