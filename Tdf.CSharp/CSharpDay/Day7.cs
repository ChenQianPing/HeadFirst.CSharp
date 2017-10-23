using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.CSharpDay
{
    internal class Parent
    {
        public string Name { get; set; }
    }

    internal class Sub : Parent { }

    public class Day7
    {
        public static void TestMethod()
        {
            // 由派生类集合强转成父类集合（正确）
            List<Sub> sublist = new List<Sub> {new Sub {Name = "zzl"}, new Sub {Name = "zhz"}};
            sublist.Cast<Parent>().ToList().ForEach(i => Console.WriteLine(i.Name));

            // 由父类集合强转成派生类集合
            List<Parent> parentlist = new List<Parent> {new Sub {Name = "zzl"}, new Sub {Name = "zhz"}};
            parentlist.Cast<Sub>().ToList().ForEach(i => Console.WriteLine(i.Name));
        }
    }

}


/*
 * 派生类集合与基类集合可以相互转换吗？
 */
