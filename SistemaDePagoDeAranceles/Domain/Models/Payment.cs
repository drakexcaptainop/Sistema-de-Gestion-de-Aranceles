namespace SistemaDePagoDeAranceles.Domain.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int EstablishmentId { get; set; }
        public int FeeId { get; set; }

        public DateTime PaymentDate { get; set; }
        public decimal AmountPaid { get; set; }
        public string PaymentMethod { get; set; }
        public string ReceiptNumber { get; set; }

        public bool Status { get; set; }

        public Establishment? Establishment { get; set; }
        public int? Fee { get; set; }
    }
}
