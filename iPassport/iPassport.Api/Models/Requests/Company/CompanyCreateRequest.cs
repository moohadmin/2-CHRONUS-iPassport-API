using System;

namespace iPassport.Api.Models.Requests
{
    /// <summary>
    /// Company Create Request model
    /// </summary>
    public class CompanyCreateRequest
    {
        /// <summary>
        /// Company Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Company Trade Name
        /// </summary>
        public string TradeName { get; set; }
        /// <summary>
        /// Company Cnpj
        /// </summary>
        public string Cnpj { get; set; }
        /// <summary>
        /// Company Address
        /// </summary>
        //TODO Txai Ajustar Validação da cidade
        public AddressCreateRequest Address { get; set; }
        /// <summary>
        /// Company Segment
        /// </summary>
        public Guid? SegmentId { get; set; }
        /// <summary>
        /// Company Is Headquarters
        /// </summary>
        public bool? IsHeadquarters { get; set; }
        /// <summary>
        /// Parent Company
        /// </summary>
        public Guid? ParentId { get; set; }
        /// <summary>
        /// Responsible Person Name
        /// </summary>
        public string ResponsiblePersonName { get; set; }
        /// <summary>
        /// Responsible Person Occupation
        /// </summary>
        public string ResponsiblePersonOccupation { get; set; }
        /// <summary>
        /// Responsible Person Email
        /// </summary>
        public string ResponsiblePersonEmail { get; set; }
        //TODO Txai Ver se téra telefone fixo tbm
        /// <summary>
        /// Responsible Person Phone
        /// </summary>
        public string ResponsiblePersonPhone { get; set; }
        /// <summary>
        /// Company Is Active
        /// </summary>
        public bool? IsActive { get; set; }
    }
}
