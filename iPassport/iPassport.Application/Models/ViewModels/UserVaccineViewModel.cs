namespace iPassport.Application.Models.ViewModels
{
    public class UserVaccineViewModel
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public int RequiredDoses { get; set; }
        public int ExpirationTime { get; set; }
        public int ImunizationTime { get; set; }
    }
}
