using Common.Domain.SharedPorts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementService.Domain.Models;
using Common.Domain.Patterns;

namespace UserManagementService.Application.RepositoryServices
{
    public interface IUserRepositoryService : IRepositoryService<User>
    {
        User? GetByUsername(string username);
        Result<User> GetById(int id);
    }
}
