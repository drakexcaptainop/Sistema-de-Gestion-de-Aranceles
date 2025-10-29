using Common.Domain.Patterns;
using Common.Application.CommonRepositoryServices;
using UserManagementService.Domain.Ports;
using Common.Domain.SharedPorts;
using UserManagementService.Domain.Models;
using UserManagementService.Application.RepositoryServices;

namespace UserManagementService.Application.ServiceFactory;

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