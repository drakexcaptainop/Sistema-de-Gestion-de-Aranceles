using SistemaDePagoDeAranceles.Application.Services.RepositoryServices;
using SistemaDePagoDeAranceles.Domain.Models;
using SistemaDePagoDeAranceles.Domain.Ports.RepositoryPorts;

namespace SistemaDePagoDeAranceles.Application.Services.Factory;

public class PaymentRepositoryServiceCreator : BaseRepositoryServiceCreator<Payment>
{
    public PaymentRepositoryServiceCreator(IDbRepository<Payment> repository) : base(repository)
    {
        
    }

    public override IRepositoryService<Payment> CreateRepositoryService()
    {
        return new  PaymentRepositoryService(_repository);
    }
}