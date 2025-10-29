using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.SharedPorts
{
    public interface IRepositoryServiceFactory<T>
    {
        public IRepositoryService<T> CreateRepositoryService();
    }
}
