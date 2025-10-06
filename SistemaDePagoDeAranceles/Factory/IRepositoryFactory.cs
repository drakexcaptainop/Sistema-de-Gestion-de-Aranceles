using SistemaDePagoDeAranceles.Respository;

namespace SistemaDePagoDeAranceles.Factory;

public interface IRepositoryFactory<T>
{
    public IDbRespository<T> CreateRepository();
}