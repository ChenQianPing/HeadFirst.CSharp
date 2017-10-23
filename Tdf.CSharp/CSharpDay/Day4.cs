using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.CSharpDay
{
    /*
     * 值类型和引用类型；
     * 
     * 值类型和栈：
     * 灵活性并不高，如果希望生命活得长久点？
     * 这个时候，堆的作用就体现出来。
     * 
     * 引用类型和堆：
     * 首先，会在栈上分配一个空间，存放引用h，它仅仅是一个引用，不是对象。
     * 到第二句进行实例化对象，new 运算符是用来请求分配储存空间的，
     * CLR会搜索堆上足够的位置，分配给对象，然后new会返回它所在堆上的地址给引用。
     * 因此在栈上存放着该引用指向堆上的对象的地址。
     * 
     * 值类型的使用减少了堆的压力，同时减少垃圾回收的次数。
     * 引用类型却弥补了生命周期的不足，增加了灵活性。 
     * 
     * 值类型创建变量时是赋予默认值的，例如int默认值是0。
     * 而引用类型创建变量，默认是null。
     * 那么，没有对象的引用类型的变量使用时会报异常NullReferenceException。
     * 
     * 
     * c1将地址复制给c2，也就是说c1和c2指向的是同一个对象，
     * 因此c1和c2其中一个修改了，另外的也会受影响。
     * 
     * s1将成员复制给s2，虽然s1和s2存储了相同的值，但是他们内存地址都不相同，
     * 存的是属于自己的值，因此s1和s2其实一个修改了，也不会影响另外一个。
     * 
     * 
     */

    // 值类型
    public struct Struct
    {
        public int X;
    }

    // 引用类型
    public class Class
    {
        public int X;
    }

    public class Day4
    {
        public void TestMethod1()
        {
            // 代码A
            var s1 = new Struct();   // 分配在栈上
            var c1 = new Class();    // 分配在堆上
            s1.X = 1;
            c1.X = 1;

            Console.WriteLine(s1.X);  // 输出1 
            Console.WriteLine(c1.X);  // 输出1

            // 代码B
            var s2 = s1;              // 复制栈上成员给s2
            var c2 = c1;              // 复制引用给c2
            s2.X = 2;                 // 值类型，s1.x不变，s2.x 变更
            c2.X = 2;                 // 引用类型，c1.x和c2.x 同时改变

            Console.WriteLine(s1.X);  // 1 值类型
            Console.WriteLine(s2.X);  // 2 值类型

            Console.WriteLine(c1.X);  // 2 引用类型
            Console.WriteLine(c2.X);  // 2 引用类型
        }

    }
}


/*
 * Value Types，值类型：
 * 在C#中，值类型继承自System.ValueType的，它们分别是
 * Bool,byte,char,decimal, double, enu, float, int, long, sbyte, short, struct, uint, ulong, ushort
 * 
 * Reference Types 引用类型
 * 引用类型包括所有的从System.Object继承下来的类型，它们分别是
 * class, interface, delegate, object,string，其中string是一种特殊的引用类型。
 * 
 * 默认值表（C# 参考）
 * https://docs.microsoft.com/zh-cn/dotnet/csharp/language-reference/keywords/default-values-table
 * 
 * 下表显示了整型的大小和范围，这些类型构成了简单类型的一个子集。
 * http://www.cnblogs.com/lori/archive/2012/11/29/2794308.html

 */

