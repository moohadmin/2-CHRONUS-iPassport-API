using System;

namespace iPassport.Api.Models.Requests.HealthUnit
{
    /// <summary>
    /// Health Unit Create Request
    /// </summary>
    public class HealthUnitCreateRequest
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Cnpj
        /// </summary>
        public string Cnpj { get; set; }

        /// <summary>
        /// Ine
        /// </summary>
        public string Ine { get; set; }

        /// <summary>
        /// E-mail
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Health Unit Responsible
        /// </summary>
        public HealthUnitResponsibleCreateRequest Responsible { get; set; }       

        /// <summary>
        /// Deactivation Date
        /// </summary>
        public DateTime? DeactivationDate { get; set; }

        /// <summary>
        /// Type Id
        /// </summary>
        public Guid? TypeId { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        public AddressCreateRequest Address { get; set; }

        /// <summary>
        /// Is Active?
        /// </summary>
        public bool? IsActive { get; set; }

        /// <summary>
        /// Company Id
        /// </summary>
        public Guid? CompanyId { get; set; }
    }
}
