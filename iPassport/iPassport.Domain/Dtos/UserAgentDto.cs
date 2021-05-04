using System;

namespace iPassport.Domain.Dtos
{
    public class UserAgentDto
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string CPF { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public AddressDto Address { get; set; }
        public Guid CompanyId { get; set; }
        public Guid Id { get; set; }
        public string CorporateCellphoneNumber { get; set; }
        public string CellphoneNumber { get; set; }
        public bool? IsActive { get; set; }
        public Guid? DeactivationUserId { get; set; }
    }
}
