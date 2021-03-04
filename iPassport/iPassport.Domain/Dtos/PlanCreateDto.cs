using System;

namespace iPassport.Domain.Dtos
{
    public class PlanCreateDto
    {
        public string Type { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Observation { get; set; }
        public Guid Id { get; set; }
        public string ColorStart { get; set; }
        public string ColorEnd { get; set; }
        public bool Active { get; set; }
    }
}
