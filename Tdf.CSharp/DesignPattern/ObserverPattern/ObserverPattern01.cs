using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.DesignPattern.ObserverPattern
{
    /*
     * 假设有一个软件公司，每当有新产品推出，就把信息通知到一些客户。
     */

    public interface IService
    {
        void Notif();
    }

    public class CustomerA : IService
    {
        public void Notif()
        {
            Console.WriteLine("客户A收到通知了~~");
        }
    }
    public class CustomerB : IService
    {
        public void Notif()
        {
            Console.WriteLine("客户B收到通知了~~");
        }
    }

    /*
     * 作为软件公司来讲，维护着一个客户的集合，并提供注册、取消注册的方法，
     * 往这个集合添加或删除客户。
     * 每当有通知的时候，就遍历客户集合，让IService执行通知。
     * 软件公司可以看作是一个被观察对象，或者说是发起动作的源头。
     */
    public class MyCompany
    {
        private readonly IList<IService> _subscribers = new List<IService>();
        public void Subscribe(IService subscriber)
        {
            _subscribers.Add(subscriber);
        }
        public void CancelSubscribe(IService subscriber)
        {
            _subscribers.Remove(subscriber);
        }
        public void SendMsg()
        {
            foreach (var service in _subscribers)
            {
                service.Notif();
            }
        }
    }

    public class ObserverPattern01
    {
        public static void TestMethod()
        {
            /*
             * 客户端创建软件公司实例、创建观察者实例、注册或取消观察者等。
             */  
            var company = new MyCompany();
            IService customerA = new CustomerA();
            IService customerB = new CustomerB();

            company.Subscribe(customerA);
            company.Subscribe(customerB);

            company.SendMsg();

            Console.ReadKey();
        }
    }
}


/*
 * 总结：
 * 把一个通知的动作抽象成接口
 * 观察者如果想接收到通知，就实现通知接口
 * 被观察对象做3件事情：维护观察者的集合，注册/取消观察者，发起动作遍历观察者集合让通知接口来做事
*/
