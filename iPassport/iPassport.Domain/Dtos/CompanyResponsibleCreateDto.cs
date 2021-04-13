namespace iPassport.Domain.Dtos
{
    public class CompanyResponsibleCreateDto
    {
        public System.Guid? CompanyId { get; set; }
        public string Name { get; set; }
        public string Occupation { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public string Landline { get; set; }
    }
}
