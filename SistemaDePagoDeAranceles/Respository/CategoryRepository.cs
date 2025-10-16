using SistemaDePagoDeAranceles.Domain.Models;
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
            string query = "UPDATE category SET last_update = CURRENT_TIMESTAMP, status = FALSE WHERE id = @Id";
            return sqlConnectionManager.ExecuteParameterizedNonQuery<Category>(query, model);
        }

        public override IEnumerable<Category> GetAll()
        {
        string query = "SELECT id as Id, name as Name, description as Description, base_amount as BaseAmount, created_date as CreatedDate, last_update as LastUpdate, status as Status, created_by as CreatedBy FROM category WHERE status = 1";
            return sqlConnectionManager.ExecuteQuery<Category>(query);
        }

        public override int Insert(Category model)
        {
            string query = "INSERT INTO category (name, description, base_amount, created_date, last_update, status, created_by) VALUES (@Name, @Description, @BaseAmount, @CreatedDate, @LastUpdate, @Status, @CreatedBy)";
            return sqlConnectionManager.ExecuteParameterizedNonQuery<Category>(query, model);

        }

        public override int Update(Category model)
        {
            string query = "UPDATE category SET name = @Name, description = @Description, base_amount = @BaseAmount, last_update = CURRENT_TIMESTAMP, created_by = @CreatedBy WHERE id = @Id";
            return sqlConnectionManager.ExecuteParameterizedNonQuery<Category>(query, model);
        }

        public override IEnumerable<Category> Search(string property)
        {
            throw new NotImplementedException();
        }
    }
}
