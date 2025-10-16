using SistemaDePagoDeAranceles.Application.Services.RepositoryServices;
using SistemaDePagoDeAranceles.Domain.Models;
using SistemaDePagoDeAranceles.Domain.Ports.RepositoryPorts;

namespace SistemaDePagoDeAranceles.Application.Services.Factory;

public class UserRepositoryServiceCreator : BaseRepositoryServiceCreator<User>
{
    public UserRepositoryServiceCreator(IDbRepository<User> repository) : base(repository)
    {
        
    }
    public override IRepositoryService<User> CreateRepositoryService()
    {
        return new UserRepositoryService(_repository);
    }
}