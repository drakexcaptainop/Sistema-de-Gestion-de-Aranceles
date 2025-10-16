using SistemaDePagoDeAranceles.Application.Services.RepositoryServices;
using SistemaDePagoDeAranceles.Domain.Models;
using SistemaDePagoDeAranceles.Domain.Ports.RepositoryPorts;

namespace SistemaDePagoDeAranceles.Application.Services.Factory;

public class PersonInChargeRepositoryServiceCreator : BaseRepositoryServiceCreator<PersonInCharge>
{
    public PersonInChargeRepositoryServiceCreator(IDbRepository<PersonInCharge> repository) : base(repository)
    {
        
    }

    public override IRepositoryService<PersonInCharge> CreateRepositoryService()
    {
        return new PersonInChargeRepositoryService(_repository);
    }
}