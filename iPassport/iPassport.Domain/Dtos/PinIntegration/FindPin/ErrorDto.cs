
namespace iPassport.Domain.Dtos.PinIntegration.FindPin
{
    /// <summary>
    /// Indicates whether the error occurred during the query execution.
    /// </summary>
    public class ErrorDto
    {
        /// <summary>
        /// Human-readable description of the error..
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Tells if the error is permanent.
        /// </summary>
        public bool? Permanent { get; set; }

        /// <summary>
        /// Error ID.
        /// </summary>
        
        public int? Id { get; set; }

        /// <summary>
        /// Error group ID.
        /// </summary>
        public int? GroupId { get; set; }

        /// <summary>
        /// Error group name.
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Error name.
        /// </summary>
        public string Name { get; set; }
    }
}
