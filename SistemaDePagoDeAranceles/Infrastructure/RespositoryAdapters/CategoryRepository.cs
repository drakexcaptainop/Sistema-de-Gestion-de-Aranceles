using SistemaDePagoDeAranceles.Domain.Ports.DabasePorts;
using SistemaDePagoDeAranceles.Domain.Ports.RepositoryPorts;
using SistemaDePagoDeAranceles.Models;

namespace SistemaDePagoDeAranceles.Infrastructure.RespositoryAdapters;

public class CategoryRepository : IDbRepository<Category>
{
    private readonly IDbConnectionManager _dbConnectionManager;
    public CategoryRepository(IDbConnectionManager dbConnectionManager)
    {
        _dbConnectionManager = dbConnectionManager;
    }
    public int Delete(Category model)
    {
        string query = "UPDATE category SET last_update = CURRENT_TIMESTAMP, active = FALSE WHERE id = @Id";
        return _dbConnectionManager.ExecuteParameterizedNonQuery<Category>(query, model);
    }

    public IEnumerable<Category> GetAll()
    {
        string query = "SELECT id as Id, name as Name, description as Description, base_amount as BaseAmount, register_date as RegisterDate, last_update as LastUpdate, active as Active, created_by as CreatedBy FROM category WHERE active = 1";
        return _dbConnectionManager.ExecuteQuery<Category>(query);
    }

    public int Insert(Category model)
    {
        string query = "INSERT INTO category (name, description, base_amount, register_date, last_update, active, created_by) VALUES (@Name, @Description, @BaseAmount, @RegisterDate, @LastUpdate, @Active, @CreatedBy)";
        return _dbConnectionManager.ExecuteParameterizedNonQuery<Category>(query, model);

    }

    public int Update(Category model)
    {
        string query = "UPDATE category SET name = @Name, description = @Description, base_amount = @BaseAmount, last_update = CURRENT_TIMESTAMP, created_by = @CreatedBy WHERE id = @Id";
        return _dbConnectionManager.ExecuteParameterizedNonQuery<Category>(query, model);
    }
}