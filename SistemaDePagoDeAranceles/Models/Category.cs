namespace SistemaDePagoDeAranceles.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal BaseAmount { get; set; }
        
        public DateTime RegisterDate { get; set; }
        public DateTime LastUpdate { get; set; }

        public bool Active { get; set; }
        public int CreatedBy { get; set; }
    }
}
