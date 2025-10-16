using SistemaDePagoDeAranceles.Domain.Models;
using SistemaDePagoDeAranceles.Domain.Ports.DabasePorts;
using SistemaDePagoDeAranceles.Domain.Ports.RepositoryPorts;

namespace SistemaDePagoDeAranceles.Infrastructure.RespositoryAdapters
{
    public class PersonInChargeRepository : IDbRepository<PersonInCharge>
    {
        private readonly IDbConnectionManager _dbConnectionManager;
        public PersonInChargeRepository(IDbConnectionManager dbConnectionManager)
        {
            _dbConnectionManager = dbConnectionManager;
        }

        public int Delete(PersonInCharge model)
        {
            string query = @"
                UPDATE person_in_charge
                SET last_update = CURRENT_TIMESTAMP,
                    status = FALSE
                WHERE id = @Id;";
            return _dbConnectionManager.ExecuteParameterizedNonQuery(query, model);
        }

        public IEnumerable<PersonInCharge> GetAll()
        {
            string query = @"
                SELECT
                    id            AS Id,
                    first_name    AS FirstName,
                    last_name     AS LastName,
                    email         AS Email,
                    phone         AS Phone,
                    ci            AS Ci,
                    address       AS Address,
                    created_by    AS CreatedBy,
                    created_date  AS CreatedDate,
                    last_update   AS LastUpdate,
                    status        AS Status
                FROM person_in_charge
                WHERE status = TRUE
                ORDER BY id DESC;";
            return _dbConnectionManager.ExecuteQuery<PersonInCharge>(query);
        }

        public int Insert(PersonInCharge model)
        {
            string query = @"
                INSERT INTO person_in_charge
                (
                    first_name,
                    last_name,
                    email,
                    phone,
                    ci,
                    address,
                    created_by,
                    created_date,
                    last_update,
                    status
                )
                VALUES
                (
                    @FirstName,
                    @LastName,
                    @Email,
                    @Phone,
                    @Ci,
                    @Address,
                    @CreatedBy,
                    CURRENT_TIMESTAMP,
                    CURRENT_TIMESTAMP,
                    @Status
                );";
            return _dbConnectionManager.ExecuteParameterizedNonQuery(query, model);
        }
        public int Update(PersonInCharge model)
        {
            string query = @"
                UPDATE person_in_charge
                SET
                    first_name   = @FirstName,
                    last_name    = @LastName,
                    email        = @Email,
                    phone        = @Phone,
                    ci           = @Ci,
                    address      = @Address,
                    created_by   = @CreatedBy,
                    last_update  = CURRENT_TIMESTAMP,
                    status       = @Status
                WHERE id = @Id;";
            return _dbConnectionManager.ExecuteParameterizedNonQuery(query, model);
        }

        public IEnumerable<PersonInCharge> Search(string property)
        {
            var probe = new PersonInCharge
            {
                FirstName = property,
                LastName = property,
                Email = property,
                Phone = property,
                Ci = property
            };

            const string query = @"
                SELECT
                    id            AS Id,
                    first_name    AS FirstName,
                    last_name     AS LastName,
                    email         AS Email,
                    phone         AS Phone,
                    ci            AS Ci,
                    address       AS Address,
                    created_by    AS CreatedBy,
                    created_date  AS CreatedDate,
                    last_update   AS LastUpdate,
                    status        AS Status
                FROM person_in_charge
                WHERE status = TRUE AND (
                    (@FirstName IS NOT NULL AND first_name LIKE CONCAT('%', @FirstName, '%')) OR
                    (@LastName IS NOT NULL AND last_name LIKE CONCAT('%', @LastName, '%')) OR
                    (@Email IS NOT NULL AND email LIKE CONCAT('%', @Email, '%')) OR
                    (@Phone IS NOT NULL AND phone LIKE CONCAT('%', @Phone, '%')) OR
                    (@Ci IS NOT NULL AND ci LIKE CONCAT('%', @Ci, '%'))
                )
                ORDER BY id DESC;";
            return _dbConnectionManager.ExecuteParameterizedQuery<PersonInCharge>(query, probe);
        }
    }
}
