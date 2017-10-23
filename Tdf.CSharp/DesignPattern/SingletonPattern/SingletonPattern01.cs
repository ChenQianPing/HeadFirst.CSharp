using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.DesignPattern.SingletonPattern
{
    public class SingletonPattern01
    {
        public static void TestMethod1()
        {
            var log01 = Logger01.GetInstance();
            log01.WriteToFile();
            Console.Read();
        }
    }

    /// <summary>
    /// 即时加载的单例模式，把类的实例赋值给类的一个静态字段
    /// </summary>
    public class Logger01
    {
        private static Logger01 _logger01 = new Logger01();
        private Logger01() { }
        public static Logger01 GetInstance()
        {
            return _logger01;
        }
        public void WriteToFile()
        {
            Console.WriteLine("错误被写入文件了~~");
        }
    }

    /*
     * 延迟加载的单例模式；
     * 直到调用类的静态方法，才产生类的实例。
     */
    public class Logger02
    {
        private static Logger02 _logger02 = null;
        private Logger02() { }
        public static Logger02 GetInstance()
        {
            if (null == _logger02)
            {
                _logger02 = new Logger02();
            }
            return _logger02;
        }
        public void WriteToFile()
        {
            Console.WriteLine("错误被写入文件了~~");
        }
    }

    /*
     * 线程安全的单例模式
     * 直到调用类的静态方法，保证只有一个线程进入产生类的实例。
     */
    public class Logger03
    {
        private static Logger03 _logger03 = null;
        private static readonly object LockObj = new object();
        private Logger03() { }
        public static Logger03 GetInstance()
        {
            lock (LockObj)
            {
                return _logger03 ?? (_logger03 = new Logger03());
            }
        }
        public void WriteToFile()
        {
            Console.WriteLine("错误被写入文件了~~");
        }
    }

}
