using SistemaDePagoDeAranceles.Models;
using SistemaDePagoDeAranceles.Database;
using SistemaDePagoDeAranceles.Factory;

namespace SistemaDePagoDeAranceles.Respository
{
    public class CategoryRepository : BaseDbRepository<Category>
    {
        public CategoryRepository(MySqlConnectionManager mySqlConnectionManager) : base (mySqlConnectionManager)
        {
            
        }
        public override int Delete(Category model)
        {
            string query = "UPDATE category SET last_update = CURRENT_TIMESTAMP, active = FALSE WHERE id = @Id";
            return sqlConnectionManager.ExecuteParameterizedNonQuery<Category>(query, model);
        }

        public override IEnumerable<Category> GetAll()
        {
        string query = "SELECT id as Id, name as Name, description as Description, base_amount as BaseAmount, register_date as RegisterDate, last_update as LastUpdate, active as Active, created_by as CreatedBy FROM category WHERE active = 1";
            return sqlConnectionManager.ExecuteQuery<Category>(query);
        }

        public override int Insert(Category model)
        {
            string query = "INSERT INTO category (name, description, base_amount, register_date, last_update, active, created_by) VALUES (@Name, @Description, @BaseAmount, @RegisterDate, @LastUpdate, @Active, @CreatedBy)";
            return sqlConnectionManager.ExecuteParameterizedNonQuery<Category>(query, model);

        }

        public override int Update(Category model)
        {
            string query = "UPDATE category SET name = @Name, description = @Description, base_amount = @BaseAmount, last_update = CURRENT_TIMESTAMP, created_by = @CreatedBy WHERE id = @Id";
            return sqlConnectionManager.ExecuteParameterizedNonQuery<Category>(query, model);
        }
    }
}
