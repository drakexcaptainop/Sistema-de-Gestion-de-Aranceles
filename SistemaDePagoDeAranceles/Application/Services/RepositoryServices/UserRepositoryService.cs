using SistemaDePagoDeAranceles.Domain.Models;
using SistemaDePagoDeAranceles.Domain.Ports.RepositoryPorts;

namespace SistemaDePagoDeAranceles.Application.Services.RepositoryServices;

public class UserRepositoryService : BaseRepositoryService<User>
{
    public UserRepositoryService(IDbRepository<User> repository) : base(repository)
    {
        
    }
}