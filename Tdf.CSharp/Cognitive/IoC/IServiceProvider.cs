using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.Cognitive.IoC
{
    /// <summary>
    /// 服务提供接口
    /// </summary>
    public interface IServiceProvider
    {
        /// <summary>
        ///  获取某个类型的服务实例
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        TService Resolve<TService>() where TService : class;
    }
}
