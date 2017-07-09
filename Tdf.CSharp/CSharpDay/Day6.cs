using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.CSharpDay
{
    /*
     * C# 中的动态编程；
     * 优先使用静态类型，静态类型比动态类型更高效，
     * 动态类型和在运行时创建表达式树都会带来性能上的影响，即便这点影响微不足道；
     * 
     * 5
     * 7
     * 9
     */
    public class Day6
    {

        public void TestMethod1()
        {
            var result1 = AddDynamic(2, 3);
            Console.WriteLine((int)result1);

            var result2 = AddFunc(3, 4, (x, y) => x + y);
            Console.WriteLine(result2);

            var result3 = AddExpressionTree(4, 5);
            Console.WriteLine(result3);
        }

        /// <summary>
        /// Add，动态
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static dynamic AddDynamic(dynamic a, dynamic b)
        {
            return a + b;
        }

        /// <summary>
        /// Add，使用委托
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TR"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        protected virtual TR AddFunc<T1, T2, TR>(T1 a, T2 b, Func<T1, T2, TR> func)
        {
            return func(a, b);
        }

        /// <summary>
        /// Add，使用表达式树
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        protected virtual T AddExpressionTree<T>(T a, T b)
        {
            var leftOperand = Expression.Parameter(typeof(T), "left");
            var rightOperand = Expression.Parameter(typeof(T), "right");
            var body = Expression.Add(leftOperand, rightOperand);
            var adder = Expression.Lambda<Func<T, T, T>>(body, leftOperand, rightOperand);

            var theDelegate = adder.Compile();
            return theDelegate(a, b);
        }

    }
}
