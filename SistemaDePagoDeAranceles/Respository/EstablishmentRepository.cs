using MySql.Data.MySqlClient;
using SistemaDePagoDeAranceles.Database;
using SistemaDePagoDeAranceles.Domain.Models;

namespace SistemaDePagoDeAranceles.Respository;

public class EstablishmentRepository : BaseDbRepository<Establishment>
{
    public EstablishmentRepository(MySqlConnectionManager mySqlConnectionManager) : base(mySqlConnectionManager)
    {
    }

    public override int Delete(Establishment model)
    {
        string query = "UPDATE establishment SET last_update = CURRENT_TIMESTAMP, status = FALSE WHERE id = @Id";
        return sqlConnectionManager.ExecuteParameterizedNonQuery<Establishment>(query, model);
    }

    public override IEnumerable<Establishment> GetAll()
    {
        string query = "SELECT id as Id, name as Name, business_name as BusinessName, tax_id as TaxId, sanitary_license as SanitaryLicense, sanitary_license_expiry as SanitaryLicenseExpiry, address as Address, phone as Phone, email as Email, establishment_type as EstablishmentType, person_in_charge_id as PersonInChargeId, created_date as CreatedDate, last_update as LastUpdate, status as Status, created_by as CreatedBy FROM establishment WHERE status = 1";
        return sqlConnectionManager.ExecuteQuery<Establishment>(query);
    }

    public override int Insert(Establishment model)
    {
        string query = "INSERT INTO establishment (name, business_name, tax_id, sanitary_license, sanitary_license_expiry, address, phone, email, establishment_type, person_in_charge_id, created_date, last_update, status, created_by) VALUES (@Name, @BusinessName, @TaxId, @SanitaryLicense, @SanitaryLicenseExpiry, @Address, @Phone, @Email, @EstablishmentType, @PersonInChargeId, @CreatedDate, @LastUpdate, @Status, @CreatedBy)";
        return sqlConnectionManager.ExecuteParameterizedNonQuery<Establishment>(query, model);
    }

    public override int Update(Establishment model)
    {
        string query = "UPDATE establishment SET name = @Name, tax_id = @TaxId, sanitary_license = @SanitaryLicense, address = @Address, phone = @Phone, email = @Email, establishment_type = @EstablishmentType, person_in_charge_id = @PersonInChargeId, last_update = CURRENT_TIMESTAMP, created_by = @CreatedBy WHERE id = @Id";
        return sqlConnectionManager.ExecuteParameterizedNonQuery<Establishment>(query, model);
    }

    public override IEnumerable<Establishment> Search(string property)
    {
        var pattern = $"%{property?.Trim() ?? string.Empty}%";

        const string sql = @"
        SELECT
            id AS Id, name AS Name, business_name AS BusinessName, tax_id AS TaxId,
            sanitary_license AS SanitaryLicense, sanitary_license_expiry AS SanitaryLicenseExpiry,
            address AS Address, phone AS Phone, email AS Email,
            establishment_type AS EstablishmentType, person_in_charge_id AS PersonInChargeId,
            created_date AS CreatedDate, last_update AS LastUpdate,
            status AS Status, created_by AS CreatedBy
        FROM establishment
        WHERE name LIKE @p
           OR business_name LIKE @p
           OR tax_id LIKE @p
           OR address LIKE @p
           OR phone LIKE @p
           OR email LIKE @p;";

        var p = new MySqlParameter("@p", MySqlDbType.VarChar) { Value = pattern };

        return sqlConnectionManager.ExecuteQuery<Establishment>(sql);
    }


}
