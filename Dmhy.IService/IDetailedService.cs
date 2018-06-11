using Dmhy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dmhy.IService
{
    public interface IDetailedService : IServiceSupport
    {
        DetailedModel GetDetailed(string Id);
    }
}
