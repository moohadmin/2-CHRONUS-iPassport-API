namespace iPassport.Api.Models
{
    /// <summary>
    /// Model that represents users profiles to authorization rules
    /// </summary>
    public class RolesModel
    {
        /// <summary>
        /// Represents admin profile
        /// </summary>
        public const string Admin = "admin";

        /// <summary>
        /// Represents business profile
        /// </summary>
        public const string Business = "business";

        /// <summary>
        /// Represents government profile
        /// </summary>
        public const string Government = "government";

        /// <summary>
        /// Represents health unity profile
        /// </summary>
        public const string HealthUnit = "healthUnit";

        /// <summary>
        /// Represents admin and business profile
        /// </summary>
        public const string AdminBusiness = "admin, business";
    }
}
