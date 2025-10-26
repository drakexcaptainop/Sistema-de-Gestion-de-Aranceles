using UserManagementService.Domain.Models;
using Common.Domain.SharedPorts;

namespace UserManagementService.Domain.Ports
{
    public interface IUserRepository : ISharedDbRepository<User>
    {
        User? GetByUsername(string username);
        User? GetById(int id);
    }
}
