using SistemaDePagoDeAranceles.Domain.Models;
using SistemaDePagoDeAranceles.Domain.Ports.RepositoryPorts;

namespace SistemaDePagoDeAranceles.Application.Services.RepositoryServices;

public class PersonInChargeRepositoryService : BaseRepositoryService<PersonInCharge>
{
    public PersonInChargeRepositoryService(IDbRepository<PersonInCharge> repository) : base(repository)
    {
        
    }
}