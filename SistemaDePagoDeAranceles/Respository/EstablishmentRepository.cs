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
        string query = "SELECT id as Id, name as Name, business_name as BusinessName, tax_id as TaxId, sanitary_license as SanitaryLicense, sanitary_license_expiry as SanitaryLicenseExpiry, address as Address, phone as Phone, email as Email, establishment_type as EstablishmentType, person_in_charge_id as PersonInChargeId, register_date as RegisterDate, last_update as LastUpdate, active as Active, created_by as CreatedBy FROM establishment WHERE active = 1";
        return sqlConnectionManager.ExecuteQuery<Establishment>(query);
    }

    public override int Insert(Establishment model)
    {
        string query = "INSERT INTO establishment (name, business_name, tax_id, sanitary_license, sanitary_license_expiry, address, phone, email, establishment_type, person_in_charge_id, register_date, last_update, active, created_by) VALUES (@Name, @BusinessName, @TaxId, @SanitaryLicense, @SanitaryLicenseExpiry, @Address, @Phone, @Email, @EstablishmentType, @PersonInChargeId, @RegisterDate, @LastUpdate, @Active, @CreatedBy)";
        return sqlConnectionManager.ExecuteParameterizedNonQuery<Establishment>(query, model);
    }

    public override int Update(Establishment model)
    {
        string query = "UPDATE establishment SET name = @Name, business_name = @BusinessName, tax_id = @TaxId, sanitary_license = @SanitaryLicense, sanitary_license_expiry = @SanitaryLicenseExpiry, address = @Address, phone = @Phone, email = @Email, establishment_type = @EstablishmentType, person_in_charge_id = @PersonInChargeId, last_update = @LastUpdate, active = @Active, created_by = @CreatedBy WHERE id = @Id";
        return sqlConnectionManager.ExecuteParameterizedNonQuery<Establishment>(query, model);
    }
}
