
using SistemaDePagoDeAranceles.Domain.Common;
using SistemaDePagoDeAranceles.Domain.Ports.RepositoryPorts;

namespace SistemaDePagoDeAranceles.Application.Services;

public class RepositoryService<TModel>
{
    IDbRepository<TModel> _repository;
    public RepositoryService(IDbRepository<TModel> repository)
    {
        _repository = repository;
    }
    public IEnumerable<TModel> GetAll()
    {
        return _repository.GetAll();
    }
    public Result<TModel> Insert(TModel model)
    {
        var inserted = _repository.Insert(model);
        if (inserted > 0)
            return Result<TModel>.Success(model);
        return Result<TModel>.Failure("No se pudo insertar el registro.");
    }

    public Result<TModel> Update(TModel model)
    {
        var updated = _repository.Update(model);
        if (updated > 0)
            return Result<TModel>.Success(model);
        return Result<TModel>.Failure("No se pudo actualizar el registro.");
    }

    public Result<TModel> Delete(TModel model)
    {
        var deleted = _repository.Delete(model);
        if (deleted > 0)
            return Result<TModel>.Success(model);
        return Result<TModel>.Failure("No se pudo eliminar el registro.");
    }
}