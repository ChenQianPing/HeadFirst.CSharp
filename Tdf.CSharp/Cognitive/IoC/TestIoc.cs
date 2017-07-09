using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.Cognitive.IoC
{
    public class TestIoc
    {
        public static void TestMethod()
        {
            Func<IServiceContainer> getContainer = () => new ServiceProvider();
            var container = getContainer();

            // 注册
            container.Register(_ => container)
                // 注册Dog实例
                .Register<IAnimal, Dog>()
                // 注册Man实例
                .Register<IPeople, Man>();

            // 得到people
            var people = container.Resolve<IPeople>();
            // 调用pet方法
            people.Pet();

            Console.ReadLine();
        }
    }
}
