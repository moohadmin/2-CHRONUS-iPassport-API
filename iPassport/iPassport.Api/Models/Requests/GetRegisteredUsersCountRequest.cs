using iPassport.Domain.Enums;
using System;

namespace iPassport.Api.Models.Requests
{
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
