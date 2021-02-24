namespace iPassport.Application.Models.ViewModels
{
    public class VaccineViewModel
    {
        public string Description { get; set; }
        public string Laboratory { get; set; }
        public int RequiredDoses { get; set; }
        public int ExpirationTime { get; set; }
        public int ImunizationTime { get; set; }
    }
}
