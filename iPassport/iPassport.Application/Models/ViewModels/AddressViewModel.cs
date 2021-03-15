using System;

namespace iPassport.Application.Models.ViewModels
{
    
    public class AddressViewModel
    {
        
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Cep { get;  set; }
        public Guid? CityId { get; set; }
        public string City { get; set; }
        public Guid? StateId { get; set; }
        public string State { get; set; }
        public Guid? CountryId { get; set; }
        public string Country { get; set; }

    }
}
