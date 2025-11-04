using SistemaDePagoDeAranceles.Domain.Models;
using SistemaDePagoDeAranceles.Domain.Ports.RepositoryPorts;

namespace SistemaDePagoDeAranceles.Application.Services.RepositoryServices;

public class PaymentRepositoryService : BaseRepositoryService<Payment>
{
    public PaymentRepositoryService(IDbRepository<Payment> repository) : base(repository)
    {
        
    }   
}