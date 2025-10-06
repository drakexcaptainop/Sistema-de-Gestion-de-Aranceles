using SistemaDePagoDeAranceles.Database;
using SistemaDePagoDeAranceles.Models;
using SistemaDePagoDeAranceles.Respository;

namespace SistemaDePagoDeAranceles.Factory;

public class ExampleTestRepositoryCreator : RepositoryFactory<Test>
{
    public ExampleTestRepositoryCreator( MySqlConnectionManager manager ) : base(manager)
    {
        
    }
    public override IDbRespository<Test> CreateRepository()
    {
        return new ExampleTestRepository( _connectionManager );
    }
}