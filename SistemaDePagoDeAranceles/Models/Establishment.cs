namespace SistemaDePagoDeAranceles.Models
{
    public enum EstablishmentType
    {
        RESTAURANT,
        BAR,
        CAFETERIA,
        FAST_FOOD,
        OTHER
    }

    public class Establishment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BusinessName { get; set; }
        public string TaxId { get; set; }
        public string SanitaryLicense { get; set; }
        public DateTime SanitaryLicenseExpiry { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public String EstablishmentType { get; set; }
        public int PersonInChargeId { get; set; }

        // Audit
        public DateTime RegisterDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool Active { get; set; }
        public int CreatedBy { get; set; }
    }
}
