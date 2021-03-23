using System;

namespace iPassport.Api.Models.Requests.HealthUnit
{
    /// <summary>
    /// Health Unit Edit Request
    /// </summary>
    public class HealthUnitEditRequest
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }

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
        /// Responsible Person Name
        /// </summary>
        public string ResponsiblePersonName { get; set; }

        /// <summary>
        /// Responsible Person Phone
        /// </summary>
        public string ResponsiblePersonPhone { get; set; }

        /// <summary>
        /// Responsible Person Occupation
        /// </summary>
        public string ResponsiblePersonOccupation { get; set; }

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
        public AddressEditRequest Address { get; set; }

        /// <summary>
        /// Is Active?
        /// </summary>
        public bool? IsActive { get; set; }
    }
}
