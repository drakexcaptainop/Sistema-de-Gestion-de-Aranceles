using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TariffingService.Domain.Models;
using Common.Domain.SharedPorts;

namespace TariffingService.Domain.RepositoryPorts
{
    public interface IFeeRepository : ISharedDbRepository<Fee>
    {
    }
}
