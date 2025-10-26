

using Common.Domain.SharedPorts;
using UserManagementService.Application.RepositoryServices;
using UserManagementService.Domain.Ports;
using UserManagementService.Domain.Models;
using Common.Domain.Patterns;
using Common.Application.CommonRepositoryServices;

namespace UserManagementService.Application.RepositoryServices;

public class UserRepositoryService : BaseRepositoryService<User>, IUserRepositoryService
{
    private readonly IUserRepository _userRepository;
    
    public UserRepositoryService(ISharedDbRepository<User> repository, IUserRepository userRepository) : base(repository)
    {
        _userRepository = userRepository;
    }
    
    public User? GetByUsername(string username)
    {
        return _userRepository.GetByUsername(username);
    }
    public Result<User> GetById(int id)
    {
        try
        {
            var user = _userRepository.GetById(id);
            if (user == null)
                return Result<User>.Failure("Usuario no encontrado.");

            return Result<User>.Success(user);
        }
        catch (Exception)
        {
            return Result<User>.Failure("Error al obtener el usuario.");
        }
    }
}