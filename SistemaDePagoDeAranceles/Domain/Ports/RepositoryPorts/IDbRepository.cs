namespace SistemaDePagoDeAranceles.Domain.Ports.RepositoryPorts;

public interface IDbRepository<T> 
{
    public IEnumerable<T> GetAll();
    public int Insert(T model);
    public int Update(T model);
    public int Delete(T model);
    public IEnumerable<T> Search(string property);
}