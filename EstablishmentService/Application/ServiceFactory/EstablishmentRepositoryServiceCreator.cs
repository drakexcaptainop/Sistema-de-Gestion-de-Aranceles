
using Common.Application.CommonRepositoryServices;
using Common.Domain.SharedPorts;

using EstablishmentService.Domain.Models;
using EstablishmentService.Application.RepositoryServices;

namespace EstablishmentService.Application.ServiceFactory;

public class EstablishmentRepositoryServiceCreator : BaseRepositoryServiceCreator<Establishment>
{
    public EstablishmentRepositoryServiceCreator(IDbRepository<Establishment> repository) : base(repository)
    {
    }
    public override IRepositoryService<Establishment> CreateRepositoryService()
    {
        return  new EstablishmentRepositoryService(_repository);
    }
}