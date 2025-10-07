using SistemaDePagoDeAranceles.Database;
using SistemaDePagoDeAranceles.Models;
using SistemaDePagoDeAranceles.Respository;

namespace SistemaDePagoDeAranceles.Factory
{
    public class CategoryRepositoryCreator : RepositoryFactory<Category>
    { 
        public CategoryRepositoryCreator( MySqlConnectionManager manager ) : base(manager)
        {
            
        }
        public override IDbRespository<Category> CreateRepository()
        {
            return new CategoryRepository( _connectionManager );
        }
    }
}
