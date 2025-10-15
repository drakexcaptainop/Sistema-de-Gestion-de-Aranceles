using SistemaDePagoDeAranceles.Domain.Ports.RepositoryPorts;
using SistemaDePagoDeAranceles.Models;

namespace SistemaDePagoDeAranceles.Application.Services.RepositoryServices;

public class CategoryRepositoryService : BaseRepositoryService<Category>
{
    public CategoryRepositoryService(IDbRepository<Category> repository) : base(repository)
    {
        
    }
}