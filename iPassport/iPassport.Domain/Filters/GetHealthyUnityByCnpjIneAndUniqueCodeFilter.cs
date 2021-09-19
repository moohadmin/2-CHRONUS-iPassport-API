namespace iPassport.Domain.Filters
{
    public class GetHealthyUnityByCnpjIneAndUniqueCodeFilter
    {
        public string Cnpj { get; set; }
        public string Ine { get; set; }
        public int? UniqueCode { get; set; }
    }
}
