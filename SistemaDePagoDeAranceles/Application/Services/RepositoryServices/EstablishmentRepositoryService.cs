using SistemaDePagoDeAranceles.Domain.Models;
using SistemaDePagoDeAranceles.Domain.Ports.RepositoryPorts;

namespace SistemaDePagoDeAranceles.Application.Services.RepositoryServices;

public class EstablishmentRepositoryService : BaseRepositoryService<Establishment>
{
    public EstablishmentRepositoryService(IDbRepository<Establishment> repository) : base(repository)
    {
        
    }
}