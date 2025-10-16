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
        string query = @"
                UPDATE category
                SET last_update = CURRENT_TIMESTAMP,
                    status = FALSE
                WHERE id = @Id;";
        return _dbConnectionManager.ExecuteParameterizedNonQuery(query, model);
    }

    public IEnumerable<Category> GetAll()
    {
        string query = @"
                SELECT
                    id             AS Id,
                    name           AS Name,
                    description    AS Description,
                    base_amount    AS BaseAmount,
                    created_by     AS CreatedBy,
                    created_date   AS CreatedDate,
                    last_update    AS LastUpdate,
                    status         AS Status
                FROM category
                WHERE status = TRUE
                ORDER BY id DESC;";
        return _dbConnectionManager.ExecuteQuery<Category>(query);
    }

    public int Insert(Category model)
    {
        string query = @"
                INSERT INTO category
                (
                    name,
                    description,
                    base_amount,
                    created_by,
                    created_date,
                    last_update,
                    status
                )
                VALUES
                (
                    @Name,
                    @Description,
                    @BaseAmount,
                    @CreatedBy,
                    CURRENT_TIMESTAMP,
                    CURRENT_TIMESTAMP,
                    @Status
                );";
        return _dbConnectionManager.ExecuteParameterizedNonQuery(query, model);

    }

    public int Update(Category model)
    {
        string query = @"
                UPDATE category
                SET
                    name         = @Name,
                    description  = @Description,
                    base_amount  = @BaseAmount,
                    created_by   = @CreatedBy,
                    last_update  = CURRENT_TIMESTAMP,
                    status       = @Status
                WHERE id = @Id;";
        return _dbConnectionManager.ExecuteParameterizedNonQuery(query, model);
    }
    public IEnumerable<Category> Search(string property)
    {
        var probe = new Category
        {
            Name = property,
            Description = property
        };

        const string query = @"
                SELECT
                    id             AS Id,
                    name           AS Name,
                    description    AS Description,
                    base_amount    AS BaseAmount,
                    created_by     AS CreatedBy,
                    created_date   AS CreatedDate,
                    last_update    AS LastUpdate,
                    status         AS Status
                FROM category
                WHERE status = TRUE
                AND (
                    (@Name IS NOT NULL AND name LIKE CONCAT('%', @Name, '%')) OR
                    (@Description IS NOT NULL AND description LIKE CONCAT('%', @Description, '%'))
                )
                ORDER BY id DESC;";
        return _dbConnectionManager.ExecuteParameterizedQuery<Category>(query, probe);
    }
}