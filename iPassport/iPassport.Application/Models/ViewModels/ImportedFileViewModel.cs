using System;

namespace iPassport.Application.Models.ViewModels
{
    /// <summary>
    /// Imported File View Model
    /// </summary>
    public class ImportedFileViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// File's Names
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// File Total Lines
        /// </summary>
        public int TotalLines { get; set; }
        /// <summary>
        /// Imported Date
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// File Status
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// User's Names
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// User's Id
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// Imported Lines
        /// </summary>
        public int ImportedLines { get; set; }
        /// <summary>
        /// Unimported Lines
        /// </summary>
        public int UnImportedLines { get; set; }

    }
}
