using MySql.Data.MySqlClient;

namespace SistemaDePagoDeAranceles.Domain.Ports.DabasePorts;

public interface IDbConnectionManager
{
    public IEnumerable<T> ExecuteParameterizedQuery<T>(string query, T model) where T : new();
    public IEnumerable<T> ExecuteQuery<T>(string query) where T : new();
    public int ExecuteParameterizedNonQuery<T>(string query, T model) where T : new();
    public int ExecuteNonQuery(string query);
}