using SistemaDePagoDeAranceles.Domain.Ports.DabasePorts;
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
    public int Insert(TModel model)
    {
        return _repository.Insert(model);
    }

    public int Update(TModel model)
    {
        return _repository.Update(model);
    }
    public int Delete(TModel model)
    {
        return _repository.Delete(model);
    }
}