using SistemaDePagoDeAranceles.Domain.Ports;
using SistemaDePagoDeAranceles.Domain.Ports.RepositoryPorts;
using SistemaDePagoDeAranceles.Infrastructure.Database;
using SistemaDePagoDeAranceles.Domain.Models;
using SistemaDePagoDeAranceles.Infrastructure.Database;

namespace SistemaDePagoDeAranceles.Infrastructure.RespositoryAdapters
{
    public class UserRepository : IDbRepository<User>
    {
        private readonly MySqlConnectionManager _dbConnectionManager;
        public UserRepository(MySqlConnectionManager dbConnectionManager)
        {
            _dbConnectionManager = dbConnectionManager;
        }

        public int Delete(User model)
        {
            string query = @"
                UPDATE user
                SET last_update = CURRENT_TIMESTAMP,
                    status = FALSE
                WHERE id = @Id;";
            return _dbConnectionManager.ExecuteParameterizedNonQuery(query, model);
        }

        public IEnumerable<User> GetAll()
        {
            string query = @"
                SELECT
                    id            AS Id,
                    username      AS Username,
                    password_hash AS PasswordHash,
                    first_name    AS FirstName,
                    last_name     AS LastName,
                    email         AS Email,
                    role          AS Role,
                    created_by    AS CreatedBy,
                    created_date  AS CreatedDate,
                    last_update   AS LastUpdate,
                    status        AS Status
                FROM user
                WHERE status = TRUE
                ORDER BY id DESC;";
            return _dbConnectionManager.ExecuteQuery<User>(query);
        }

        public int Insert(User model)
        {
            string query = @"
                INSERT INTO user
                (
                    username,
                    password_hash,
                    first_name,
                    last_name,
                    email,
                    role,
                    created_by,
                    created_date,
                    last_update,
                    status
                )
                VALUES
                (
                    @Username,
                    @PasswordHash,
                    @FirstName,
                    @LastName,
                    @Email,
                    @Role,
                    @CreatedBy,
                    CURRENT_TIMESTAMP,
                    CURRENT_TIMESTAMP,
                    @Status
                );";
            return _dbConnectionManager.ExecuteParameterizedNonQuery(query, model);
        }
        public int Update(User model)
        {
            string query = @"
                UPDATE user
                SET
                    username      = @Username,
                    password_hash = @PasswordHash,
                    first_name    = @FirstName,
                    last_name     = @LastName,
                    email         = @Email,
                    role          = @Role,
                    created_by    = @CreatedBy,
                    last_update   = CURRENT_TIMESTAMP,
                    status        = @Status
                WHERE id = @Id;";
            return _dbConnectionManager.ExecuteParameterizedNonQuery(query, model);
        }

        public IEnumerable<User> Search(string property)
        {
            var probe = new User
            {
                Username = property,
                FirstName = property,
                LastName = property,
                Email = property,
                Role = property
            };

            const string query = @"
                SELECT
                    id            AS Id,
                    username      AS Username,
                    password_hash AS PasswordHash,
                    first_name    AS FirstName,
                    last_name     AS LastName,
                    email         AS Email,
                    role          AS Role,
                    created_by    AS CreatedBy,
                    created_date  AS CreatedDate,
                    last_update   AS LastUpdate,
                    status        AS Status
                FROM user
                WHERE status = TRUE AND (
                    (@Username IS NOT NULL AND username LIKE CONCAT('%', @Username, '%')) OR
                    (@FirstName IS NOT NULL AND first_name LIKE CONCAT('%', @FirstName, '%')) OR
                    (@LastName IS NOT NULL AND last_name LIKE CONCAT('%', @LastName, '%')) OR
                    (@Email IS NOT NULL AND email LIKE CONCAT('%', @Email, '%')) OR
                    (@Role IS NOT NULL AND role LIKE CONCAT('%', @Role, '%'))
                )
                ORDER BY id DESC;";
            return _dbConnectionManager.ExecuteParameterizedQuery<User>(query, probe);
        }

        public User? GetByUsername(string username)
        {
            const string sql = @"
                SELECT id AS Id, username AS Username, password_hash AS PasswordHash,
                       first_name AS FirstName, last_name AS LastName, email AS Email,
                       role AS Role, created_by AS CreatedBy, created_date AS CreatedDate,
                       last_update AS LastUpdate, status AS Status
                FROM `user`
                WHERE username = @Username
                LIMIT 1;";
            var probe = new User { Username = username };
            return _dbConnectionManager.ExecuteParameterizedQuery<User>(sql, probe).FirstOrDefault();
        }
    }
}
