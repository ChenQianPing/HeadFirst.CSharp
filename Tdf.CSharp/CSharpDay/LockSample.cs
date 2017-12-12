using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.CSharpDay
{
    public class LockSample
    {
        private readonly object _thisLock = new object();
        public int Balance;
        private readonly Random _r = new Random();

        public LockSample(int initial)
        {
            Balance = initial;
        }

        protected virtual int WithDraw(int amount)
        {
            if (Balance < 0)
            {
                throw new Exception("负的Balance.");
            }

            /* 
             * 确保只有一个线程使用资源，一个进入临界状态,
             * 使用对象互斥锁，10个启动了的线程不能全部执行该方法
             */

            lock (_thisLock)
            {
                if (Balance >= amount)
                {
                    Console.WriteLine("----------------------------:" + System.Threading.Thread.CurrentThread.Name +
                                      "---------------");

                    Console.WriteLine("调用Withdrawal之前的Balance:" + Balance);
                    Console.WriteLine("把Amount输入 Withdrawal     :-" + amount);
                    /*
                     * 如果没有加对象互斥锁，则可能10个线程都执行下面的减法，
                     * 加减法所耗时间片段非常小，可能多个线程同时执行，出现负数。
                     */
                    Balance = Balance - amount;
                    Console.WriteLine("调用Withdrawal之后的Balance :" + Balance);


                    return amount;


                }
                else
                {
                    // 最终结果
                    return 0;
                }
            }



        }

        public void DoTransactions()
        {
            for (var i = 0; i < 100; i++)
            {
                // 生成balance的被减数amount的随机数
                WithDraw(_r.Next(1, 100));
            }
        }

        
    }

    public class TestLockSample
    {
        public void TestMethod()
        {
            // 初始化10个线程
            var threads = new System.Threading.Thread[10];

            // 把balance初始化设定为1000
            var acc = new LockSample(1000);

            for (var i = 0; i < 10; i++)
            {
                var t = new System.Threading.Thread(new System.Threading.ThreadStart(acc.DoTransactions));
                threads[i] = t;
                threads[i].Name = "Thread" + i.ToString();
            }

            for (var i = 0; i < 10; i++)
            {
                threads[i].Start();
            }
            Console.ReadKey();
        }
    }
}



