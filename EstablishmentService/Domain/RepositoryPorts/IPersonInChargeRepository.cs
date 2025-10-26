using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EstablishmentService.Domain.Models;
using Common.Domain.SharedPorts;

namespace EstablishmentService.Domain.RepositoryPorts
{
    internal interface IPersonInChargeRepository : ISharedDbRepository<PersonInCharge>
    {
    }
}
