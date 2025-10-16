using SistemaDePagoDeAranceles.Database;
using SistemaDePagoDeAranceles.Domain.Models;

namespace SistemaDePagoDeAranceles.Respository;

public class PersonInChargeRepository : BaseDbRepository<PersonInCharge>
{
    public PersonInChargeRepository(MySqlConnectionManager mySqlConnectionManager) : base(mySqlConnectionManager)
    {
    }
    public override int Delete(PersonInCharge model)
    {
        string query = "UPDATE person_in_charge SET last_update = CURRENT_TIMESTAMP, active = FALSE WHERE id = @Id";
        return sqlConnectionManager.ExecuteParameterizedNonQuery<PersonInCharge>(query, model);
    }

    public override IEnumerable<PersonInCharge> GetAll()
    {
        string query = "SELECT id as Id, first_name as FirstName, last_name as LastName, email as Email, phone as Phone, ci as Ci, register_date as RegisterDate, last_update as UpdateDate, active as Status, created_by as CreatedBy FROM person_in_charge WHERE active = 1";
        return sqlConnectionManager.ExecuteQuery<PersonInCharge>(query);
    }

    public override int Insert(PersonInCharge model)
    {
        string query = "INSERT INTO person_in_charge (first_name, last_name, email, phone, ci, register_date, last_update, active, created_by) VALUES (@FirstName, @LastName, @Email, @Phone, @Ci, @RegisterDate, @UpdateDate, @Status, @CreatedBy)";
        return sqlConnectionManager.ExecuteParameterizedNonQuery<PersonInCharge>(query, model);
    }

    public override int Update(PersonInCharge model)
    {
        string query = "UPDATE person_in_charge SET first_name = @FirstName, last_name = @LastName, email = @Email, phone = @Phone, ci = @Ci, last_update = CURRENT_TIMESTAMP, created_by = @CreatedBy WHERE id = @Id";
        return sqlConnectionManager.ExecuteParameterizedNonQuery<PersonInCharge>(query, model);
    }
    public override IEnumerable<PersonInCharge> Search(string property)
    {
        throw new NotImplementedException();
    }
}