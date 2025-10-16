using SistemaDePagoDeAranceles.Domain.Models;
using SistemaDePagoDeAranceles.Domain.Ports.DabasePorts;
using SistemaDePagoDeAranceles.Domain.Ports.RepositoryPorts;

namespace SistemaDePagoDeAranceles.Infrastructure.RespositoryAdapters
{
    public class FeeRepository : IDbRepository<Fee>
    {
        private readonly IDbConnectionManager _dbConnectionManager;
        public FeeRepository(IDbConnectionManager dbConnectionManager)
        {
            _dbConnectionManager = dbConnectionManager;
        }

        public int Delete(Fee model)
        {
            string query = @"
                DELETE FROM fee
                WHERE id = @Id;";
            return _dbConnectionManager.ExecuteParameterizedNonQuery(query, model);
        }

        public IEnumerable<Fee> GetAll()
        {
            string query = @"
                SELECT
                    id           AS Id,
                    category_id  AS CategoryId,
                    year         AS Year,
                    amount       AS Amount,
                    due_date     AS DueDate,
                    description  AS Description
                FROM fee
                ORDER BY year DESC, id DESC;";
            return _dbConnectionManager.ExecuteQuery<Fee>(query);
        }

        public int Insert(Fee model)
        {
            string query = @"
                INSERT INTO fee
                (
                    category_id,
                    year,
                    amount,
                    due_date,
                    description
                )
                VALUES
                (
                    @CategoryId,
                    @Year,
                    @Amount,
                    @DueDate,
                    @Description
                );";
            return _dbConnectionManager.ExecuteParameterizedNonQuery(query, model);
        }

        public int Update(Fee model)
        {
            string query = @"
                UPDATE fee
                SET
                    category_id = @CategoryId,
                    year        = @Year,
                    amount      = @Amount,
                    due_date    = @DueDate,
                    description = @Description
                WHERE id = @Id;";
            return _dbConnectionManager.ExecuteParameterizedNonQuery(query, model);
        }

        public IEnumerable<Fee> Search(string property)
        {
            var probe = new Fee
            {
                Description = property
            };

            string query = @"
                SELECT
                    id           AS Id,
                    category_id  AS CategoryId,
                    year         AS Year,
                    amount       AS Amount,
                    due_date     AS DueDate,
                    description  AS Description
                FROM fee
                WHERE
                    (@Description IS NOT NULL AND description LIKE CONCAT('%', @Description, '%')) OR
                    (CAST(@Description AS SIGNED) IS NOT NULL AND year = CAST(@Description AS SIGNED))
                ORDER BY year DESC, id DESC;";
            return _dbConnectionManager.ExecuteParameterizedQuery<Fee>(query, probe);
        }
    }
}
