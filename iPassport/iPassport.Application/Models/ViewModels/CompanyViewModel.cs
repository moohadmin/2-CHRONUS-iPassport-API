namespace iPassport.Application.Models.ViewModels
{
    /// <summary>
    /// Company View Model
    /// </summary>
    public class CompanyViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public System.Guid Id { get; set; }
        /// <summary>
        /// Company's Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Company CNPJ
        /// </summary>
        public string Cnpj { get; set; }
        /// <summary>
        /// Address Id
        /// </summary>
        public System.Guid AddressId { get; set; }
    }
}
