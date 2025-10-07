using SistemaDePagoDeAranceles.Database;
using SistemaDePagoDeAranceles.Models;
using SistemaDePagoDeAranceles.Respository;

namespace SistemaDePagoDeAranceles.Factory
{
    public class PersonInChargeRepositoryCreator : RepositoryFactory<PersonInCharge>
    {
        public PersonInChargeRepositoryCreator(MySqlConnectionManager manager) : base(manager)
        {
        }

        public override IDbRespository<PersonInCharge> CreateRepository()
        {
            return new PersonInChargeRepository(_connectionManager);
        }
    }
}
