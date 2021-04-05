using System;

namespace iPassport.Domain.Dtos
{
    public class AdminDto
    {
        public Guid? Id { get; set; }
        public string CompleteName { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? HealthUnitId { get; set; }
        public string Occupation { get; set; }
        public string Password { get; set; }
        public Guid? ProfileId { get; set; }
        public bool? IsActive { get; set; }
    }
}
