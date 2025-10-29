using PaymentService.Domain.Models;
using PaymentService.Domain.RepositoryPorts;
using Common.Domain.SharedPorts;
using Common.Application.CommonRepositoryServices;

namespace PaymentService.Application.RepositoryServices;

public class PaymentRepositoryService : BaseRepositoryService<Payment>
{
    public PaymentRepositoryService(IDbRepository<Payment> repository) : base(repository)
    {
        
    }   
}