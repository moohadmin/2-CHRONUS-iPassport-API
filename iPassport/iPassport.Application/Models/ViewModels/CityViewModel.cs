

namespace iPassport.Application.Models.ViewModels
{
    /// <summary>
    /// City View Model
    /// </summary>
    public class CityViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public System.Guid Id { get; set; }
        /// <summary>
        /// City's Names
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// City Ibge Code
        /// </summary>
        public int IbgeCode { get; set; }
        /// <summary>
        /// City Population
        /// </summary>
        public long? Population { get; set; }

        /// <summary>
        /// State Id
        /// </summary>
        public System.Guid StateId { get; set; }
        
    }
}
