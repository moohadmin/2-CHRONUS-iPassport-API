using System;

namespace iPassport.Api.Models.Requests.User
{
    /// <summary>
    /// User Disease Test Create Request
    /// </summary>
    public class UserDiseaseTestCreateRequest
    {
        
        /// <summary>
        /// Test Result
        /// </summary>
        public bool? Result { get;  set; }
        /// <summary>
        /// Test Date
        /// </summary>
        public DateTime TestDate { get;  set; }
        /// <summary>
        /// Result Date
        /// </summary>
        public DateTime? ResultDate { get;  set; }
    }
}