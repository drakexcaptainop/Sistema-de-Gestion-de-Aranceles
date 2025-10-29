
using Common.Domain.SharedPorts;
using Common.Application.CommonRepositoryServices;

using TariffingService.Domain.Models;
using TariffingService.Domain.RepositoryPorts;

namespace TariffingService.Application.RepositoryServices;

public class FeeRepositoryService : BaseRepositoryService<Fee>
{
    public FeeRepositoryService(IDbRepository<Fee> repository) : base(repository)
    {
        
    }
}