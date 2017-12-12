using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tdf.CSharp.Cognitive.ParallelTest
{
    public class ParallelDemo02
    {
        private readonly Stopwatch _stopWatch = new Stopwatch();

        public void Run1()
        {
            Thread.Sleep(2000);
            Console.WriteLine("Task 1 is cost 2 sec");
            throw new Exception("Exception in task 1");
        }
        public void Run2()
        {
            Thread.Sleep(3000);
            Console.WriteLine("Task 2 is cost 3 sec");
            throw new Exception("Exception in task 2");
        }

        public void ParallelInvokeMethod()
        {
            _stopWatch.Start();

            try
            {
                Parallel.Invoke(Run1, Run2);
            }
            catch (AggregateException aex)
            {
                foreach (var ex in aex.InnerExceptions)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            _stopWatch.Stop();
            Console.WriteLine("Parallel run " + _stopWatch.ElapsedMilliseconds + " ms.");

            _stopWatch.Reset();
            _stopWatch.Start();
            try
            {
                Run1();
                Run2();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            _stopWatch.Stop();
            Console.WriteLine("Normal run " + _stopWatch.ElapsedMilliseconds + " ms.");

            /*
             * Task 1 is cost 2 sec
             * Task 2 is cost 3 sec
             * Exception in task 2
             * Exception in task 1
             * Parallel run 3104 ms.
             * 
             * Task 1 is cost 2 sec
             * Exception in task 1
             * Normal run 2006 ms.
             * 
             * 顺序调用方法我把异常处理写一起了，
             * 这样只能捕获Run1的异常信息，大家可以分开写。
             * 捕获AggregateException 异常后，用foreach循环遍历输出异常信息，
             * 可以看到两个异常信息都显示了。



             */
        }
    }
}
