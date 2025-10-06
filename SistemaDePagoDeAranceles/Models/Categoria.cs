namespace SistemaDePagoDeAranceles.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Name  { get; set; }
        public string Description { get; set; }
        public bool SanitaryLicenseState { get; set; }
        public DateTime DateRegister { get; set; }
        public DateTime LastUpdate { get; set; }
        public string Status { get; set; }
    }
}
