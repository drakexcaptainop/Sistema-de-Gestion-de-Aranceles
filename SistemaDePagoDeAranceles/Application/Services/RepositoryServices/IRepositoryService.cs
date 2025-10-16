namespace SistemaDePagoDeAranceles.Application.Services.RepositoryServices;

public interface IRepositoryService<TModel>
{
    public IEnumerable<TModel> GetAll();
    public int Insert(TModel model);
    public int Update(TModel model);
    public int Delete(TModel model);
    public IEnumerable<TModel> Search(string property);
}