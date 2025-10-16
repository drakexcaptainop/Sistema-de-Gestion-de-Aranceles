using SistemaDePagoDeAranceles.Domain.Common;
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

    public Result<T> Insert(T model)
    {
        var inserted = _repository.Insert(model);
        if (inserted > 0)
            return Result<T>.Success(model);
        return Result<T>.Failure("No se pudo insertar el registro.");
    }

    public Result<T> Update(T model)
    {
        var updated = _repository.Update(model);
        if (updated > 0)
            return Result<T>.Success(model);
        return Result<T>.Failure("No se pudo actualizar el registro.");
    }

    public Result<T> Delete(T model)
    {
        var deleted = _repository.Delete(model);
        if (deleted > 0)
            return Result<T>.Success(model);
        return Result<T>.Failure("No se pudo eliminar el registro.");
    }

    public IEnumerable<T> Search(string property)
    {
        return _repository.Search(property);
    }
}