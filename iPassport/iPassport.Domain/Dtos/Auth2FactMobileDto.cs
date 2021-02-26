namespace iPassport.Domain.Dtos
{
    public class Auth2FactMobileDto
    {
        public System.Guid UserId { get; set; }
        public string Phone { get; set; }
        public string Pin { get; set; }
        public bool IsValid { get; set; }
        public string MessageId { get; set; }
    }
}