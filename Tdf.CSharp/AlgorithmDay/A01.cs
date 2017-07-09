using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.AlgorithmDay
{
    /*
     * 数据结构的重要性.
     */
    public static class A01
    {
        /*
         * 创建一个十万次的for循环，循环过程中判断当前i值是否在list集合中，
         * 如果不在，就将其加入到集合中去。
         * 通过结果我们可以看到一个如此简单的逻辑耗时竟然高达281348毫秒，将近281秒的时间。
         */
        public static void A0101()
        {
            var sw = new Stopwatch();
            sw.Start();
            var list = new List<string>();
            for (var i = 0; i < 100000; i++)
            {
                if (!list.Contains(i.ToString()))
                {
                    list.Add(i.ToString());
                }
            }
            Console.WriteLine(sw.ElapsedMilliseconds);
            Console.ReadKey();
        }

        /*
         * 我们将List集合改为HashSet，将循环次数修改为循环一百万次，
         * 发现结果不过是3173毫秒，性能得到了显著地提升.
         */
        public static void A0102()
        {
            var sw = new Stopwatch();
            sw.Start();
            var hashSet = new HashSet<string>();
            for (var i = 0; i < 1000000; i++)
            {
                if (!hashSet.Contains(i.ToString())) // 如果当前i值不在hashSet集合中
                {
                    hashSet.Add(i.ToString());       // 将当前i值添加到该集合中去
                }
            }
            Console.WriteLine(sw.ElapsedMilliseconds);
            Console.ReadKey();
        }

        /*
         * 我们将List集合改为Dictionary<string,string>，
         * 将循环次数修改为循环一百万次，
         * 发现结果不过是4246毫秒，性能同样得到了显著地提升。
         */
        public static void A0103()
        {
            var sw = new Stopwatch();
            sw.Start();
            var dic = new Dictionary<string, string>();
            for (var i = 0; i < 1000000; i++)
            {
                if (!dic.ContainsKey(i.ToString()))     // 如果当前i值不在字典中
                {
                    dic.Add(i.ToString(), i.ToString()); // 将当前i值添加到该字典中去
                }
            }
            Console.WriteLine(sw.ElapsedMilliseconds);
            Console.ReadKey();
        }


    }
}
