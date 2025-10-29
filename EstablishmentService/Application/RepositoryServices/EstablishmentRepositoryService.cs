

using EstablishmentService.Domain.Models;
using EstablishmentService.Domain.RepositoryPorts;
using Common.Domain.SharedPorts;
using Common.Application.CommonRepositoryServices;

namespace EstablishmentService.Application.RepositoryServices;

public class EstablishmentRepositoryService : BaseRepositoryService<Establishment>
{
    public EstablishmentRepositoryService(IDbRepository<Establishment> repository) : base(repository)
    {
        
    }
}