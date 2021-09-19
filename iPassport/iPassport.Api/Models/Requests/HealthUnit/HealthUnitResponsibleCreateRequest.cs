namespace iPassport.Api.Models.Requests
{
    /// <summary>
    /// Health Unit Responsible Create Request Model
    /// </summary>
    public class HealthUnitResponsibleCreateRequest
    {
        /// <summary>
        /// Responsible Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Responsible Occupation
        /// </summary>
        public string Occupation { get; set; }
        /// <summary>
        /// Responsible Mobile Phone
        /// </summary>
        public string MobilePhone { get; set; }
        /// <summary>
        /// Responsible Landline
        /// </summary>
        public string Landline { get; set; }
    }
}
