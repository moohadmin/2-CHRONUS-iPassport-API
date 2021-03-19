using iPassport.Domain.Entities;
using System;

namespace iPassport.Domain.Dtos
{
    public class CityDto
    {
        public CityDto(City city)
        {
            Id = city?.Id;
            Name = city?.Name;
            IbgeCode = city?.IbgeCode;
            Population = city?.Population;
            State = new StateDto(city?.State);
        }

        public Guid? Id { get; set; }
        public string Name { get; set; }
        public int? IbgeCode { get; set; }
        public int? Population { get; set; }

        public StateDto State { get; set; }
    }
}