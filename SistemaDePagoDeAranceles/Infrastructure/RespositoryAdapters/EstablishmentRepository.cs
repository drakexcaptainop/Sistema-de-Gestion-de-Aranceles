using SistemaDePagoDeAranceles.Domain.Ports;
using SistemaDePagoDeAranceles.Domain.Ports.RepositoryPorts;
using SistemaDePagoDeAranceles.Domain.Models;
using SistemaDePagoDeAranceles.Infrastructure.Database;

namespace SistemaDePagoDeAranceles.Infrastructure.RespositoryAdapters
{
    public class EstablishmentRepository : IDbRepository<Establishment>
    {
        private readonly MySqlConnectionManager _dbConnectionManager;
        public EstablishmentRepository(MySqlConnectionManager dbConnectionManager)
        {
            _dbConnectionManager = dbConnectionManager;
        }
        public int Delete(Establishment model)
        {
            string query = "UPDATE category SET last_update = CURRENT_TIMESTAMP, active = FALSE WHERE id = @Id";
            return _dbConnectionManager.ExecuteParameterizedNonQuery<Establishment>(query, model);
        }

        public IEnumerable<Establishment> GetAll()
        {
            string query = @"
                SELECT
                    id                         AS Id,
                    name                       AS Name,
                    tax_id                     AS TaxId,
                    sanitary_license           AS SanitaryLicense,
                    sanitary_license_expiry    AS SanitaryLicenseExpiry,
                    address                    AS Address,
                    phone                      AS Phone,
                    email                      AS Email,
                    establishment_type         AS EstablishmentType,
                    person_in_charge_id        AS PersonInChargeId,
                    created_by                 AS CreatedBy,
                    created_date               AS CreatedDate,
                    last_update                AS LastUpdate,
                    status                     AS Status
                FROM establishment
                WHERE status = TRUE
                ORDER BY id DESC;";
            return _dbConnectionManager.ExecuteQuery<Establishment>(query);
        }

        public int Insert(Establishment model)
        {
            string query = @"
                INSERT INTO establishment
                (
                    name,
                    tax_id,
                    sanitary_license,
                    sanitary_license_expiry,
                    address,
                    phone,
                    email,
                    establishment_type,
                    person_in_charge_id,
                    created_by,
                    created_date,
                    last_update,
                    status
                )
                VALUES
                (
                    @Name,
                    @TaxId,
                    @SanitaryLicense,
                    @SanitaryLicenseExpiry,
                    @Address,
                    @Phone,
                    @Email,
                    @EstablishmentType,
                    @PersonInChargeId,
                    @CreatedBy,
                    CURRENT_TIMESTAMP,
                    CURRENT_TIMESTAMP,
                    @Status
                );";
            return _dbConnectionManager.ExecuteParameterizedNonQuery(query, model);
        }
        public int Update(Establishment model)
        {
            string query = @"
                UPDATE establishment
                SET
                    name                    = @Name,
                    tax_id                  = @TaxId,
                    sanitary_license        = @SanitaryLicense,
                    sanitary_license_expiry = @SanitaryLicenseExpiry,
                    address                 = @Address,
                    phone                   = @Phone,
                    email                   = @Email,
                    establishment_type      = @EstablishmentType,
                    person_in_charge_id     = @PersonInChargeId,
                    -- status is included so you can re-enable if needed; remove if you prefer not to expose it here
                    status                  = @Status,
                    last_update             = CURRENT_TIMESTAMP
                WHERE id = @Id;";
            return _dbConnectionManager.ExecuteParameterizedNonQuery(query, model);
        }

        public IEnumerable<Establishment> Search(string property)
        {
            var probe = new Establishment
            {
                Name = property,
                TaxId = property,
                SanitaryLicense = property,
                Address = property,
                Phone = property,
                Email = property,
                EstablishmentType = property
            };

            const string query = @"
                SELECT
                    id                         AS Id,
                    name                       AS Name,
                    tax_id                     AS TaxId,
                    sanitary_license           AS SanitaryLicense,
                    sanitary_license_expiry    AS SanitaryLicenseExpiry,
                    address                    AS Address,
                    phone                      AS Phone,
                    email                      AS Email,
                    establishment_type         AS EstablishmentType,
                    person_in_charge_id        AS PersonInChargeId,
                    created_by                 AS CreatedBy,
                    created_date               AS CreatedDate,
                    last_update                AS LastUpdate,
                    status                     AS Status
                FROM establishment
                WHERE status = TRUE AND (
                    (@Name IS NOT NULL AND name              LIKE CONCAT('%', @Name, '%')) OR
                    (@TaxId IS NOT NULL AND tax_id           LIKE CONCAT('%', @TaxId, '%')) OR
                    (@SanitaryLicense IS NOT NULL AND sanitary_license LIKE CONCAT('%', @SanitaryLicense, '%')) OR
                    (@Address IS NOT NULL AND address        LIKE CONCAT('%', @Address, '%')) OR
                    (@Phone IS NOT NULL AND phone            LIKE CONCAT('%', @Phone, '%')) OR
                    (@Email IS NOT NULL AND email            LIKE CONCAT('%', @Email, '%')) OR
                    (@EstablishmentType IS NOT NULL AND establishment_type LIKE CONCAT('%', @EstablishmentType, '%'))
                )
                ORDER BY id DESC;";

            return _dbConnectionManager.ExecuteParameterizedQuery<Establishment>(query, probe);
        }
    }
}
