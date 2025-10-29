
using EstablishmentService.Domain.Models;
using EstablishmentService.Domain.RepositoryPorts;
using Common.Domain.SharedPorts;
using Common.Application.CommonRepositoryServices;

namespace EstablishmentService.Application.RepositoryServices;

public class PersonInChargeRepositoryService : BaseRepositoryService<PersonInCharge>
{
    public PersonInChargeRepositoryService(IDbRepository<PersonInCharge> repository) : base(repository)
    {
        
    }
}