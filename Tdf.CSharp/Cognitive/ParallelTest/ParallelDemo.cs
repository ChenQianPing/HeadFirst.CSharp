using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tdf.CSharp.Cognitive.ParallelTest
{
    public class ParallelDemo
    {
        private readonly Stopwatch _stopWatch = new Stopwatch();

        public void Run1()
        {
            Thread.Sleep(2000);
            Console.WriteLine("Task 1 is cost 2 sec");
        }
        public void Run2()
        {
            Thread.Sleep(3000);
            Console.WriteLine("Task 2 is cost 3 sec");
        }

        public void ParallelInvokeMethod()
        {
            _stopWatch.Start();
            Parallel.Invoke(Run1, Run2);
            _stopWatch.Stop();
            Console.WriteLine("Parallel run " + _stopWatch.ElapsedMilliseconds + " ms.");

            _stopWatch.Restart();
            Run1();
            Run2();
            _stopWatch.Stop();
            Console.WriteLine("Normal run " + _stopWatch.ElapsedMilliseconds + " ms.");

            /*
             * 
             * Task 1 is cost 2 sec
             * Task 2 is cost 3 sec
             * Parallel run 3109 ms.
             * Task 1 is cost 2 sec
             * Task 2 is cost 3 sec
             * Normal run 5013 ms.
             * 
             * 大家应该能够猜到，正常调用的话应该是5秒多，
             * 而Parallel.Invoke方法调用用了只有3秒，
             * 也就是耗时最长的那个方法，可以看出方法是并行执行的，执行效率提高了很多。
             */
        }


        public void ParallelForMethod()
        {
            _stopWatch.Start();
            for (var i = 0; i < 10000; i++)
            {
                for (var j = 0; j < 60000; j++)
                {
                    var sum = 0;
                    sum += i;
                }
            }
            _stopWatch.Stop();
            Console.WriteLine("NormalFor run " + _stopWatch.ElapsedMilliseconds + " ms.");

            _stopWatch.Reset();
            _stopWatch.Start();
            Parallel.For(0, 10000, item =>
            {
                for (var j = 0; j < 60000; j++)
                {
                    var sum = 0;
                    sum += item;
                }
            });
            _stopWatch.Stop();
            Console.WriteLine("ParallelFor run " + _stopWatch.ElapsedMilliseconds + " ms.");


            /*
             * NormalFor run 7867 ms.
             * ParallelFor run 2142 ms.
             * 
             * 可以看到，Parallel.For所用的时间比单纯的for快了5秒多，
             * 可见提升的性能是非常可观的。
             * 那么，是不是Parallel.For在任何时候都比for要快呢？
             * 答案当然是“不是”，要不然微软还留着for干嘛？
             */

        }


        public void ParallelForMethod02()
        {
            var obj = new object();
            long num = 0;
            var bag = new ConcurrentBag<long>();

            _stopWatch.Start();
            for (var i = 0; i < 10000; i++)
            {
                for (var j = 0; j < 60000; j++)
                {
                    num++;
                }
            }
            _stopWatch.Stop();
            Console.WriteLine("NormalFor run " + _stopWatch.ElapsedMilliseconds + " ms.");

            _stopWatch.Reset();
            _stopWatch.Start();
            Parallel.For(0, 10000, item =>
            {
                for (var j = 0; j < 60000; j++)
                {
                    lock (obj)
                    {
                        num++;
                    }
                }
            });
            _stopWatch.Stop();
            Console.WriteLine("ParallelFor run " + _stopWatch.ElapsedMilliseconds + " ms.");

            /*
             * NormalFor run 10474 ms.
             * ParallelFor run 32262 ms.
             * 
             * Parallel.For由于是并行运行的，
             * 所以会同时访问全局变量num,为了得到正确的结果，要使用lock,
             * 此时来看看运行结果：
             * 
             * Parallel.For竟然用了32秒多，而for跟之前的差不多。
             * 这主要是由于并行同时访问全局变量，会出现资源争夺，
             * 大多数时间消耗在了资源等待上面。
             * 
             * Parallel.For比For慢不是慢在资源的等待上，而是任务的调度上。

             */

        }

        public void TestMetod01()
        {
            Parallel.For(0, 100, i =>
            {
                Console.Write(i + "\t");
            });

            /*
             * 0       50      75      25      26      27      3       1       51      76
             * 28      4       2       6       7       52      53      54      55      56
             * 77      78      5       29      8       9       14      15      16      17
             * 57      79      10      30      31      32      33      34      35      36
             * 18      58      59      80      11      12      13      37      19      60
             * 81      82      83      84      85      86      22      23      38      20
             * 61      62      63      87      88      89      39      24      21      40
             * 64      90      65      66      67      41      91      92      93      94
             * 95      68      42      43      44      45      96      97      69      70
             * 46      47      48      49      98      99      71      72      73      74
             * 
             * 
             * 从0输出到99，运行后会发现输出的顺序不对，用for顺序肯定是对的，
             * 并行同时执行，所以会出现输出顺序不同的情况。
             */
        }

        public void ParallelBreak()
        {
            var bag = new ConcurrentBag<int>();
            _stopWatch.Start();
            Parallel.For(0, 1000, (i, state) =>
            {
                if (bag.Count == 300)
                {
                    state.Break();
                    return;
                }
                bag.Add(i);
            });
            _stopWatch.Stop();
            Console.WriteLine("Bag count is " + bag.Count + ", " + _stopWatch.ElapsedMilliseconds);

            /*
             * Stop：
             * Bag count is 302, 66
             * Bag count is 301, 51
             * 
             * Break：
             * Bag count is 303, 52
             * 
             * 这里使用的是Stop，当数量达到300个时，会立刻停止；
             * 可以看到结果"Bag count is 300"，
             * 
             * 使用Stop 也不一定是 Bag count is 300，
             * 只能做到接近或等于300；

             * 
             * 如果用break,可能结果是300多个或者300个，大家可以测试一下。


             */
        }

    }
}


/*
 * 当我们使用到Parallel，必然是处理一些比较耗时的操作，当然也很耗CPU和内存，
 * 如果我们中途向停止，怎么办呢？
 * 
 * 并行任务的顺序是随机的；
 * 并行编程，本质上是多线程的编程，那么当多个线程同时处理一个任务的时候，
 * 必然会出现资源访问问题，及所谓的线程安全。
 * 
 * 就像现实中，我们开发项目，就是一个并行的例子，把不同的模块分给不同的人，
 * 同时进行，才能在短的时间内做出大的项目。如果大家都只管自己写自己的代码，
 * 写完后发现合并不到一起，那么这种并行就没有了意义。
 * 

 */
