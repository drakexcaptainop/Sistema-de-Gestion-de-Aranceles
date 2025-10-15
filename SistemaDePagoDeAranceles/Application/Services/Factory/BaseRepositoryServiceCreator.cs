using SistemaDePagoDeAranceles.Application.Services.RepositoryServices;
using SistemaDePagoDeAranceles.Domain.Ports.RepositoryPorts;

namespace SistemaDePagoDeAranceles.Application.Services.Factory;

public abstract class BaseRepositoryServiceCreator<T> : IRepositoryServiceFactory<T>
{
    protected readonly IDbRepository<T>  _repository;

    public BaseRepositoryServiceCreator( IDbRepository<T> repository )
    {
        _repository = repository;
    }
    public abstract IRepositoryService<T> CreateRepositoryService();
}