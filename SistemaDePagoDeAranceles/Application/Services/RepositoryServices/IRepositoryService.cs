using SistemaDePagoDeAranceles.Domain.Common;

namespace SistemaDePagoDeAranceles.Application.Services.RepositoryServices;

public interface IRepositoryService<TModel>
{
    public IEnumerable<TModel> GetAll();
    public Result<TModel> Insert(TModel model);
    public Result<TModel> Update(TModel model);
    public Result<TModel> Delete(TModel model);
    public IEnumerable<TModel> Search(string property);
}