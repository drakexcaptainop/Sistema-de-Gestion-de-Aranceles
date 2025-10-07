using SistemaDePagoDeAranceles.Database;
using SistemaDePagoDeAranceles.Models;
using SistemaDePagoDeAranceles.Respository;

namespace SistemaDePagoDeAranceles.Factory
{
    public class EstablishmentRepositoryCreator : RepositoryFactory<Establishment>
    {
        public EstablishmentRepositoryCreator(MySqlConnectionManager manager) : base(manager)
        {
        }

        public override IDbRespository<Establishment> CreateRepository()
        {
            return new EstablishmentRepository(_connectionManager);
        }
    }
}
