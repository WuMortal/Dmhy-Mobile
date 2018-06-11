using Dmhy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dmhy.IService
{
    public interface IDramaService : IServiceSupport
    {
        /// <summary>
        /// 获取 每个星期的番剧
        /// </summary>
        /// <returns></returns>
        DramaIndexModel[] GetDramaData();
    }
}
