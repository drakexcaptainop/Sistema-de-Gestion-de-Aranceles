using SistemaDePagoDeAranceles.Database;
using SistemaDePagoDeAranceles.Respository;
namespace SistemaDePagoDeAranceles.Factory;

public abstract class RepositoryFactory<T> : IRepositoryFactory<T>
{
    protected readonly MySqlConnectionManager _connectionManager;
    public RepositoryFactory(MySqlConnectionManager manager)
    {
        _connectionManager = manager;
    }
    public abstract IDbRespository<T> CreateRepository();
}


