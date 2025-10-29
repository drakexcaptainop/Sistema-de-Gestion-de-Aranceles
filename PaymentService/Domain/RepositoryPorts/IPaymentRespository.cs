using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentService.Domain.Models;
using Common.Domain.SharedPorts;

namespace PaymentService.Domain.RepositoryPorts
{
    public interface IPaymentRespository : IDbRepository<Payment>
    {
    }
}
