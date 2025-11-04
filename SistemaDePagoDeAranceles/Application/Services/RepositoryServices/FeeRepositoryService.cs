using SistemaDePagoDeAranceles.Domain.Models;
using SistemaDePagoDeAranceles.Domain.Ports.RepositoryPorts;

namespace SistemaDePagoDeAranceles.Application.Services.RepositoryServices;

public class FeeRepositoryService : BaseRepositoryService<Fee>
{
    public FeeRepositoryService(IDbRepository<Fee> repository) : base(repository)
    {
        
    }
}