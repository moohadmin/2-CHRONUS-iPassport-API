using System;

namespace iPassport.Application.Models.ViewModels
{
    public class AgentDetailsViewModel
    {
        public Guid Id { get; set; }
        public string CompleteName { get; set; }
        public string Username { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string CellphoneNumber { get; set; }
        public string CorporateCellphoneNumber { get; set; }
        public AddressViewModel Address { get; set; }
        public Guid? CompanyId { get; set; }
        public string CompanyName { get; set; }        
        public bool? IsActive { get; set; }
    }
}
