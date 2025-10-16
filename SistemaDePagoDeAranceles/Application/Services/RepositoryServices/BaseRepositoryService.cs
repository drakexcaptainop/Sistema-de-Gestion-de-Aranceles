using SistemaDePagoDeAranceles.Domain.Ports.RepositoryPorts;

namespace SistemaDePagoDeAranceles.Application.Services.RepositoryServices;

public abstract class BaseRepositoryService<T> : IRepositoryService<T>
{
    protected readonly IDbRepository<T> _repository;
    public BaseRepositoryService(IDbRepository<T> repository)
    {
        _repository = repository;
    }
    public IEnumerable<T> GetAll()
    {
        return _repository.GetAll();
    }

    public int Insert(T model)
    {
        return _repository.Insert(model);
    }

    public int Update(T model)
    {
        return _repository.Update(model);
    }

    public int Delete(T model)
    {
        return _repository.Delete(model);
    }

    public IEnumerable<T> Search(string property)
    {
        return _repository.Search(property);
    }
}