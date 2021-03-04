namespace iPassport.Api.Models.Requests
{
    public class PlanCreateRequest
    {
        public string Type { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Observation { get; set; }
    }
}
