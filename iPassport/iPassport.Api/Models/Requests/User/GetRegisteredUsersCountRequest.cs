namespace iPassport.Api.Models.Requests
{
    /// <summary>
    /// Get Registered Users Count Request
    /// </summary>
    public class GetRegisteredUsersCountRequest
    {
        /// <summary>
        /// Type of User to Get Count
        /// 1 - Citizen
        /// 2 - Agent
        /// </summary>
        public int ProfileType { get; set; }
    }
}
