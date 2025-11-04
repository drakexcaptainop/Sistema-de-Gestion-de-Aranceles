using SistemaDePagoDeAranceles.Application.Services.RepositoryServices;
using SistemaDePagoDeAranceles.Domain.Models;
using SistemaDePagoDeAranceles.Domain.Ports.RepositoryPorts;

namespace SistemaDePagoDeAranceles.Application.Services.Factory;

public class FeeRepositoryServiceCreator : BaseRepositoryServiceCreator<Fee>
{
    public FeeRepositoryServiceCreator(IDbRepository<Fee> repository) : base(repository)
    {
        
    }

    public override IRepositoryService<Fee> CreateRepositoryService()
    {
        return new FeeRepositoryService(_repository);
    }
}