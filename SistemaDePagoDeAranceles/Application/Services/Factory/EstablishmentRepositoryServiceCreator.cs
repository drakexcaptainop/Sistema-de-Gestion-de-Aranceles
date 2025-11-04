using SistemaDePagoDeAranceles.Application.Services.RepositoryServices;
using SistemaDePagoDeAranceles.Domain.Models;
using SistemaDePagoDeAranceles.Domain.Ports.RepositoryPorts;

namespace SistemaDePagoDeAranceles.Application.Services.Factory;

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