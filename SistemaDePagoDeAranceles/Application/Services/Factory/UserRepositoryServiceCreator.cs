using SistemaDePagoDeAranceles.Application.Services.RepositoryServices;
using SistemaDePagoDeAranceles.Domain.Models;
using SistemaDePagoDeAranceles.Domain.Ports.RepositoryPorts;

namespace SistemaDePagoDeAranceles.Application.Services.Factory;

public class UserRepositoryServiceCreator : BaseRepositoryServiceCreator<User>
{
    private readonly IUserRepository _userRepository;
    
    public UserRepositoryServiceCreator(IDbRepository<User> repository, IUserRepository userRepository) : base(repository)
    {
        _userRepository = userRepository;
    }
    
    public override IRepositoryService<User> CreateRepositoryService()
    {
        return new UserRepositoryService(_repository, _userRepository);
    }
}