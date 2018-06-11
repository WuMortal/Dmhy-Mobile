using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dmhy.IService
{
    /// <summary>
    /// 一个标识接口，所有服务都必须实现这个接口
    /// 这样可以保证只有真正的服务实现类才会被注册到autofac
    /// </summary>
    public interface IServiceSupport
    {
    }
}
