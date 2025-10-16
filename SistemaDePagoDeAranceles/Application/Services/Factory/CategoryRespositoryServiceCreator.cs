using SistemaDePagoDeAranceles.Application.Services.RepositoryServices;
using SistemaDePagoDeAranceles.Domain.Ports.RepositoryPorts;
using SistemaDePagoDeAranceles.Domain.Models;

namespace SistemaDePagoDeAranceles.Application.Services.Factory;

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