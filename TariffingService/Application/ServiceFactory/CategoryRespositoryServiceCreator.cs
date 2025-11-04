using Common.Application.CommonRepositoryServices;
using Common.Domain.SharedPorts;
using TariffingService.Domain.Models;
using TariffingService.Application.RepositoryServices;

namespace TariffingService.Application.ServiceFactory;

public class CategoryRespositoryServiceCreator : BaseRepositoryServiceCreator<Category>
{
    public CategoryRespositoryServiceCreator(IDbRepository<Category> repository) : base(repository)
    {
    }

    public override IRepositoryService<Category> CreateRepositoryService()
    {
        return new CategoryRepositoryService( _repository );
    }
}