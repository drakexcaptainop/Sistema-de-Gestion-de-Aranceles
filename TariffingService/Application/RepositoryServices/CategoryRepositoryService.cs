

using Common.Domain.SharedPorts;
using Common.Application.CommonRepositoryServices;

using TariffingService.Domain.Models;
using TariffingService.Domain.RepositoryPorts;

namespace TariffingService.Application.RepositoryServices;

public class CategoryRepositoryService : BaseRepositoryService<Category>
{
    public CategoryRepositoryService(IDbRepository<Category> repository) : base(repository)
    {
        
    }
}