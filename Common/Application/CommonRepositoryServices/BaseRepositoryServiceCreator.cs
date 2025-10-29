using Common.Domain.SharedPorts;

namespace Common.Application.CommonRepositoryServices;

public abstract class BaseRepositoryServiceCreator<T> : IRepositoryServiceFactory<T>
{
    protected readonly IDbRepository<T>  _repository;

    public BaseRepositoryServiceCreator( IDbRepository<T> repository )
    {
        _repository = repository;
    }
    public abstract IRepositoryService<T> CreateRepositoryService();
}