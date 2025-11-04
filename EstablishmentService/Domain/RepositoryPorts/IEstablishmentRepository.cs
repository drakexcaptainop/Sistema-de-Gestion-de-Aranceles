using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Domain.SharedPorts;
using EstablishmentService.Domain.Models;

namespace EstablishmentService.Domain.RepositoryPorts
{
    public interface IEstablishmentRepository : IDbRepository<Establishment>
    {
    }
}
