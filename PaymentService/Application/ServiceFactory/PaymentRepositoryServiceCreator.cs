

using Common.Domain.SharedPorts;
using Common.Application.CommonRepositoryServices;
using PaymentService.Domain.Models;
using PaymentService.Application.RepositoryServices;

namespace PaymentService.Application.ServiceFactory;

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