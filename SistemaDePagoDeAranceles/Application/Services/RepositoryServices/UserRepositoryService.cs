using SistemaDePagoDeAranceles.Domain.Models;
using SistemaDePagoDeAranceles.Domain.Ports.RepositoryPorts;

namespace SistemaDePagoDeAranceles.Application.Services.RepositoryServices;

public interface IUserRepositoryService : IRepositoryService<User>
{
    User? GetByUsername(string username);
}

public class UserRepositoryService : BaseRepositoryService<User>, IUserRepositoryService
{
    private readonly IUserRepository _userRepository;
    
    public UserRepositoryService(IDbRepository<User> repository, IUserRepository userRepository) : base(repository)
    {
        _userRepository = userRepository;
    }
    
    public User? GetByUsername(string username)
    {
        return _userRepository.GetByUsername(username);
    }
}