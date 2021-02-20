using System;

namespace iPassport.Domain.Dtos
{
    public class PlanCreateDto
    {
        public string Type { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid Id { get; set; }
    }
}
