using SistemaDePagoDeAranceles.Domain.Ports;
using SistemaDePagoDeAranceles.Domain.Ports.RepositoryPorts;
using SistemaDePagoDeAranceles.Infrastructure.Database;
using SistemaDePagoDeAranceles.Domain.Models;
using SistemaDePagoDeAranceles.Infrastructure.Database;

namespace SistemaDePagoDeAranceles.Infrastructure.RespositoryAdapters
{
    public class PaymentRepository : IDbRepository<Payment>
    {
        private readonly MySqlConnectionManager _dbConnectionManager;
        public PaymentRepository(MySqlConnectionManager dbConnectionManager)
        {
            _dbConnectionManager = dbConnectionManager;
        }

        public int Delete(Payment model)
        {
            string query = @"
                UPDATE payment
                SET status = 'Cancelled'
                WHERE id = @Id;";
            return _dbConnectionManager.ExecuteParameterizedNonQuery(query, model);
        }

        public IEnumerable<Payment> GetAll()
        {
            string query = @"
                SELECT
                    id               AS Id,
                    establishment_id AS EstablishmentId,
                    fee_id           AS FeeId,
                    payment_date     AS PaymentDate,
                    amount_paid      AS AmountPaid,
                    payment_method   AS PaymentMethod,
                    receipt_number   AS ReceiptNumber,
                    status           AS Status
                FROM payment
                ORDER BY id DESC;";
            return _dbConnectionManager.ExecuteQuery<Payment>(query);
        }

        public int Insert(Payment model)
        {
            string query = @"
                INSERT INTO payment
                (
                    establishment_id,
                    fee_id,
                    payment_date,
                    amount_paid,
                    payment_method,
                    receipt_number,
                    status
                )
                VALUES
                (
                    @EstablishmentId,
                    @FeeId,
                    @PaymentDate,
                    @AmountPaid,
                    @PaymentMethod,
                    @ReceiptNumber,
                    @Status
                );";
            return _dbConnectionManager.ExecuteParameterizedNonQuery(query, model);
        }

        public int Update(Payment model)
        {
            string query = @"
                UPDATE payment
                SET
                    establishment_id = @EstablishmentId,
                    fee_id           = @FeeId,
                    payment_date     = @PaymentDate,
                    amount_paid      = @AmountPaid,
                    payment_method   = @PaymentMethod,
                    receipt_number   = @ReceiptNumber,
                    status           = @Status
                WHERE id = @Id;";
            return _dbConnectionManager.ExecuteParameterizedNonQuery(query, model);
        }

        public IEnumerable<Payment> Search(string property)
        {
            var probe = new Payment
            {
                PaymentMethod = property,
                ReceiptNumber = property,
            };

            const string query = @"
                SELECT
                    id               AS Id,
                    establishment_id AS EstablishmentId,
                    fee_id           AS FeeId,
                    payment_date     AS PaymentDate,
                    amount_paid      AS AmountPaid,
                    payment_method   AS PaymentMethod,
                    receipt_number   AS ReceiptNumber,
                    status           AS Status
                FROM payment
                WHERE
                    (@PaymentMethod IS NOT NULL AND payment_method LIKE CONCAT('%', @PaymentMethod, '%')) OR
                    (@ReceiptNumber IS NOT NULL AND receipt_number LIKE CONCAT('%', @ReceiptNumber, '%')) OR
                    (@Status IS NOT NULL AND status LIKE CONCAT('%', @Status, '%'))
                ORDER BY id DESC;";
            return _dbConnectionManager.ExecuteParameterizedQuery<Payment>(query, probe);
        }
    }
}
