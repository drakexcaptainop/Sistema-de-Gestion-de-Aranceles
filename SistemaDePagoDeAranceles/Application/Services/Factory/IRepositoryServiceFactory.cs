using SistemaDePagoDeAranceles.Application.Services.RepositoryServices;

namespace SistemaDePagoDeAranceles.Application.Services.Factory;

public interface IRepositoryServiceFactory<T>
{
    public IRepositoryService<T> CreateRepositoryService();
}