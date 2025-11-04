
using Common.Application.CommonRepositoryServices;
using Common.Domain.SharedPorts;

using EstablishmentService.Domain.Models;
using EstablishmentService.Application.RepositoryServices;

namespace EstablishmentService.Application.ServiceFactory;

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