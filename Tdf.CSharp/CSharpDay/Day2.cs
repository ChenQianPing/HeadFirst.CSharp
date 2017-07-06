using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.CSharpDay
{
    public delegate void GreetingDelegate(string name); // 声明委托

    public class Day2
    {
        // 定义事件处理程序
        private static void ChineseGreeting(string name)  
        {
            Console.WriteLine("你好, " + name);
        }

        public static void TestMethod1()
        {
            var gm = new GreetingManager();

            // 订阅事件（将事件处理程序添加到事件的调用列表中）
            gm.MakeGreet += ChineseGreeting;        
            gm.GreetPeople("事件！");
            Console.ReadKey();
        }
    }

    public class GreetingManager
    {
        // 声明一个事件，event为声明事件的关键字，其实事件是一种特殊类型的委托；
        public event GreetingDelegate MakeGreet;  

        public void GreetPeople(string name)
        {
            // 把这里的事件看做一个委托来理解，更容易点。
            if (MakeGreet != null) MakeGreet(name);
        }
    }
}
