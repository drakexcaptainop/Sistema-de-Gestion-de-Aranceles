using Microsoft.Extensions.Primitives;
using SistemaDePagoDeAranceles.Database;
using SistemaDePagoDeAranceles.Models;

namespace SistemaDePagoDeAranceles.Respository
{
    public class ExampleTestRepository : BaseDbRepository<Test>
    {
        public ExampleTestRepository(MySqlConnectionManager mySqlConnectionManager) : base(mySqlConnectionManager)
        {
        }

        public override int Delete(Test model)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Test> GetAll()
        {

            string query = "SELECT nombre as Name, descripcion as Description FROM test";
            return sqlConnectionManager.ExecuteQuery<Test>(query);
        }

        public override int Insert(Test model)
        {
            string query = "INSERT INTO test (nombre, descripcion) VALUES (@Name, @Description)";
            return sqlConnectionManager.ExecuteParameterizedNonQuery<Test>(query, model);
        }

        public override int Update(Test model)
        {
            throw new NotImplementedException();
        }
    }
}
