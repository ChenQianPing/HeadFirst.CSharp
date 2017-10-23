using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.CSharpDay
{

    /// <summary>
    /// 基类
    /// </summary>
    public abstract class Parent01
    {
        public string Name { get; set; }
    }
    /// <summary>
    /// 派生类1
    /// </summary>
    public class Sub1 : Parent01 { }

    /// <summary>
    /// 派生类2
    /// </summary>
    public class Sub2 : Parent01 { }

    public class Day8
    {

        public static void TestMethod()
        {
            var parents = new Parent01[]
            {
                new Sub1 {Name = "sub1 class"},
                new Sub2 {Name = "sub2 class"},
            };

            foreach (var parent in parents)
            {
                Test(parent);
            }

            Console.ReadKey();
        }

        /// <summary>
        /// 面向抽象编程（抽象类的多态性）
        /// </summary>
        /// <param name="entity"></param>
        public static void Test(Parent01 entity)
        {
            Console.WriteLine(entity.GetType());
        }

    }
}
