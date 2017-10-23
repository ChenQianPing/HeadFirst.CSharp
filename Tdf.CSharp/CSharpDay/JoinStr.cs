using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.CSharpDay
{
    public class JoinStr
    {
        /*
         * 多用StringBuilder，少用字符串拼接
         * 在C#中，在处理字符串拼接的时候，
         * 使用StringBuilder的效率会比硬拼接字符串高很多。到底有多高，如下：
         * 
         * 
         * 拼接字符串所耗费时间为：381毫秒
         * 使用StringBuilder所耗费时间为：7毫秒
         * 
         * string类型的特别之处在于我们可以像使用值类型那样使用string类型，
         * 而实际上string是引用类型。
         * 既然是引用类型，CLR就会把string类型保存在托管堆上。
         * 当我们使用str1 = str1 + i.ToString();进行拼接，
         * 由于string类型的恒定性，不会改变str1在内存中的地址，
         * 而是在托管堆上创建了另外一个字符串对象。
         * 如此，拼接10000次，就创建了10000个string类型对象，效率难免低下。
         * 
         * 而StringBuilder会在内存中开辟一块连续的内存，
         * 当增加字符串实际上是针对同一块内存的修改，所以效率更高。  
         * 
         * 当然，到底使用硬拼接字符串，还是使用StringBuilder，不是绝对的，
         * 要看情况。当拼接字符串很少的情况下，当然直接硬拼接字符串就行了。    
         */
        public static void TestMethod()
        {
            var str1 = string.Empty;
            var sw1 = new Stopwatch();
            sw1.Start();
            for (var i = 0; i < 10000; i++)
            {
                str1 = str1 + i.ToString();
            }
            sw1.Stop();
            Console.WriteLine("拼接字符串所耗费时间为：" + sw1.ElapsedMilliseconds + "毫秒");

            var str2 = new StringBuilder(10000);
            var sw2 = new Stopwatch();
            sw2.Start();
            for (var i = 0; i < 10000; i++)
            {
                str2.Append(i.ToString());
            }
            sw2.Stop();
            Console.WriteLine("使用StringBuilder所耗费时间为：" + sw2.ElapsedMilliseconds + "毫秒");
            Console.ReadKey();
        }
    }
}
