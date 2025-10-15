
using SistemaDePagoDeAranceles.Database;
namespace SistemaDePagoDeAranceles.Respository
{
    public abstract class BaseDbRepository<T> : IDbRespository<T>
    {
        protected readonly MySqlConnectionManager sqlConnectionManager;
        public BaseDbRepository( MySqlConnectionManager mySqlConnectionManager )
        {
            sqlConnectionManager = mySqlConnectionManager;
        }
        public abstract int Delete(T model);

        public abstract IEnumerable<T> GetAll();

        public abstract int Insert(T model);

        public abstract int Update(T model);

        public abstract IEnumerable<T> Search(string property);
    }
}
