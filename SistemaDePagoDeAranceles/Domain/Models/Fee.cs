namespace SistemaDePagoDeAranceles.Domain.Models
{
    public class Fee
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int Year { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public string Description { get; set; }
        public Category? Category { get; set; }
    }
}
