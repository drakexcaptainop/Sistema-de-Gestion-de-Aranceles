using Common.Application.CommonRepositoryServices;
using Common.Domain.SharedPorts;
using TariffingService.Domain.Models;
using TariffingService.Application.RepositoryServices;

namespace TariffingService.Application.ServiceFactory;

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