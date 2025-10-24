using SistemaDePagoDeAranceles.Domain.Models;

namespace SistemaDePagoDeAranceles.Domain.Ports.RepositoryPorts
{
    public interface IUserRepository
    {
        User? GetByUsername(string username);
        User? GetById(int id);
    }
}
