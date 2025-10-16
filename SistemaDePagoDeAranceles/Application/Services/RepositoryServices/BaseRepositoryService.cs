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
    public Result<IEnumerable<T>> GetAll()
    {
        try
        {
            IEnumerable<T> items = _repository.GetAll();
            if (!items.Any())
                return Result<IEnumerable<T>>.Failure("No se tienen registros");

            return Result<IEnumerable<T>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IEnumerable<T>>.Failure($"Ocurrio un error al obtener los datos");
        }
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

    public Result<IEnumerable<T>> Search(string property)
    {
        try
        {
            IEnumerable<T> items = _repository.Search(property);
            if (!items.Any())
                return Result<IEnumerable<T>>.Failure("No se encontraron registros de coincidencia");

            return Result<IEnumerable<T>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IEnumerable<T>>.Failure($"Ocurrio un error al obtener los datos");
        }
    }
}