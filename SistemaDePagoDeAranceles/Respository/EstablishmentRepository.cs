using MySql.Data.MySqlClient;
using SistemaDePagoDeAranceles.Database;
using SistemaDePagoDeAranceles.Models;

namespace SistemaDePagoDeAranceles.Respository;

public class EstablishmentRepository : BaseDbRepository<Establishment>
{
    public EstablishmentRepository(MySqlConnectionManager mySqlConnectionManager) : base(mySqlConnectionManager)
    {
    }

    public override int Delete(Establishment model)
    {
        string query = "DELETE FROM establishment WHERE id = @Id";
        return sqlConnectionManager.ExecuteParameterizedNonQuery<Establishment>(query, model);
    }

    public override IEnumerable<Establishment> GetAll()
    {
        string query = "SELECT id as Id, name as Name, business_name as BusinessName, tax_id as TaxId, sanitary_license as SanitaryLicense, sanitary_license_expiry as SanitaryLicenseExpiry, address as Address, phone as Phone, email as Email, establishment_type as EstablishmentType, person_in_charge_id as PersonInChargeId, register_date as RegisterDate, last_update as LastUpdate, active as Active, created_by as CreatedBy FROM establishment";
        return sqlConnectionManager.ExecuteQuery<Establishment>(query);
    }

    public override int Insert(Establishment model)
    {
        string query = "INSERT INTO establishment (name, business_name, tax_id, sanitary_license, sanitary_license_expiry, address, phone, email, establishment_type, person_in_charge_id, register_date, last_update, active, created_by) VALUES (@Name, @BusinessName, @TaxId, @SanitaryLicense, @SanitaryLicenseExpiry, @Address, @Phone, @Email, @EstablishmentType, @PersonInChargeId, @RegisterDate, @LastUpdate, @Active, @CreatedBy)";
        return sqlConnectionManager.ExecuteParameterizedNonQuery<Establishment>(query, model);
    }

    public override int Update(Establishment model)
    {
        string query = "UPDATE establishment SET name = @Name, business_name = @BusinessName, tax_id = @TaxId, sanitary_license = @SanitaryLicense, sanitary_license_expiry = @SanitaryLicenseExpiry, address = @Address, phone = @Phone, email = @Email, establishment_type = @EstablishmentType, person_in_charge_id = @PersonInChargeId, register_date = @RegisterDate, last_update = @LastUpdate, active = @Active, created_by = @CreatedBy WHERE id = @Id";
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
            register_date AS RegisterDate, last_update AS LastUpdate,
            active AS Active, created_by AS CreatedBy
        FROM establishment
        WHERE name LIKE @p
           OR business_name LIKE @p
           OR tax_id LIKE @p
           OR address LIKE @p
           OR phone LIKE @p
           OR email LIKE @p;";

        var p = new MySqlParameter("@p", MySqlDbType.VarChar) { Value = pattern };

        return sqlConnectionManager.ExecuteQuery<Establishment>(sql, p);
    }


}
