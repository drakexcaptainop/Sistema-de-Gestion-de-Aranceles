

using Common.Domain.SharedPorts;
using UserManagementService.Application.RepositoryServices;
using UserManagementService.Domain.Ports;
using UserManagementService.Domain.Models;
using Common.Domain.Patterns;
using Common.Application.CommonRepositoryServices;

namespace UserManagementService.Application.RepositoryServices;

public class UserRepositoryService : IUserRepositoryService
{
    private readonly IUserRepository _userRepository;
    
    public UserRepositoryService(IDbRepository<User> repository, IUserRepository userRepository)
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

    public Result<IEnumerable<User>> GetAll()
    {
        try
        {
            IEnumerable<User> items = _userRepository.GetAll();
            if (!items.Any())
                return Result<IEnumerable<User>>.Failure("No se tienen registros");

            return Result<IEnumerable<User>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IEnumerable<User>>.Failure($"Ocurrio un error al obtener los datos");
        }
    }

    public Result<User> Insert(User model)
    {
        var inserted = _userRepository.Insert(model);
        if (inserted > 0)
            return Result<User>.Success(model);
        return Result<User>.Failure("No se pudo insertar el registro.");
    }

    public Result<User> Update(User model)
    {
        var updated = _userRepository.Update(model);
        if (updated > 0)
            return Result<User>.Success(model);
        return Result<User>.Failure("No se pudo actualizar el registro.");
    }

    public Result<User> Delete(User model)
    {
        var deleted = _userRepository.Delete(model);
        if (deleted > 0)
            return Result<User>.Success(model);
        return Result<User>.Failure("No se pudo eliminar el registro.");
    }

    public Result<IEnumerable<User>> Search(string property)
    {
        try
        {
            IEnumerable<User> items = _userRepository.Search(property);
            if (!items.Any())
                return Result<IEnumerable<User>>.Failure("No se encontraron registros de coincidencia");

            return Result<IEnumerable<User>>.Success(items);
        }
        catch (Exception)
        {
            return Result<IEnumerable<User>>.Failure($"Ocurrio un error al obtener los datos");
        }
    }
}