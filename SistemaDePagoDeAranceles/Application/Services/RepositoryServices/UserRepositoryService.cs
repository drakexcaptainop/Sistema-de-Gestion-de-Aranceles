using SistemaDePagoDeAranceles.Domain.Common;
using SistemaDePagoDeAranceles.Domain.Models;
using SistemaDePagoDeAranceles.Domain.Ports.RepositoryPorts;

namespace SistemaDePagoDeAranceles.Application.Services.RepositoryServices;

public interface IUserRepositoryService : IRepositoryService<User>
{
    User? GetByUsername(string username);
    Result<User> GetById(int id);
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