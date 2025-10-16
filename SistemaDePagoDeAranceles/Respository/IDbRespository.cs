using SistemaDePagoDeAranceles.Domain.Models;

namespace SistemaDePagoDeAranceles.Respository
{
    public interface IDbRespository<T>
    {
        public IEnumerable<T> GetAll();
        public int Insert(T model);
        public int Update(T model);
        public int Delete(T model);
        public IEnumerable<T> Search(string property);
    }
}
