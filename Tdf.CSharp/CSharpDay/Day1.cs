using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.CSharpDay
{
    /*
     * 委托：把方法当做参数进行传递。
     * 委托的声明，delegate为声明委托的关键字；
     * public为修饰符，还可以有的修饰符为 protected、internal、private、new；
     * new修饰符，只能在其他类型中声明委托时使用，他表示所标明的委托会隐藏具有相同名称的继承成员；
     */
    public delegate void D(int i, int j);

    public delegate void HelloWorld(string name);  // 声明委托

    public class Day1
    {
        public void TestMethod1()
        {
            // 委托实例化可以创建委托类型的实例，并向该实例注册方法列表；
            // 委托类型的实例方法列表可以为静态方法，实例方法，或者另外一个委托实例。
            var d1 = new D(U.F1);
            var d2 = new D(U.F2);
            var d3 = d1 + d2;        // 这样表示d3实例依次调用U类的F1、F2方法
            var d4 = d3 - d1;

            d1(200, 100); // 输出为300，200+100=300；
            d2(200, 100); // 100，200-100=100；

            /* 60 40  这里由于实例方法列表中有2个方法，
             * 也就调用执行了2个方法，故导出2个值
             * 50+10=60；
             * 50-10=40；
             */

            d3(50, 10);   

            d4(70, 40);   // 30，70-40=30；
            Console.ReadKey();
        }

        public void TestMethod2()
        {
            // 最初的委托
            HelloWorld firstDelegate = new Day1().Hello;  // 这里可以直接写入方法。
            firstDelegate("最初最简单的,");

            // 使用匿名方法，匿名方法:委托及调用委托的简化版。
            HelloWorld sedelegate = delegate (string name) { Console.WriteLine(name + ",你好！"); };
            sedelegate("匿名方法");

            // 使用拉姆达（Lambda）表达式:匿名方法的进一步进化。
            HelloWorld thdelegate = (p => { Console.WriteLine(p + ",你好"); });
            thdelegate("拉姆达表达式");

            // 使用Action
            Action<string> fordelegate = (p => { Console.WriteLine(p + ",你好！"); });
            fordelegate("Action");

            // 使用Func
            Func<string, string> fidelegate = (p => p + ",你好！");
            var sayHello = fidelegate("Func");
            Console.WriteLine(sayHello);

            Console.ReadKey();
        }

        public void Hello(string name)
        {
            Console.WriteLine(name + "你好！");
        }
    }

    public class U
    {
        /*
         * 创建向委托注册的方法；
         * 如果一个方法能够注册到某一个委托中，那么该方法的签名必须与该委托所指定的签名完全匹配；
         * 匹配规则有2：返回类型必须相同；方法参数必须相同。
         */

        public static void F1(int a, int b)
        {
            Console.WriteLine((a + b).ToString());
        }

        public static void F2(int w, int j)
        {
            Console.WriteLine((w - j).ToString());
        }
    }


}
