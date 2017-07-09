using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.Cognitive.EventBus.Chapter2
{
    public static class OrderScene
    {
        public static void Mock()
        {
            var bus = EventBus.Instance();

            var order = new OrderEntity() { OrderId = "20151017001", OrderDateTime = DateTime.Now, OrderAmount = 500 };

            // 发布OrderAddedEvent事件；
            bus.Publish(new OrderAddedEvent() { EventTime = DateTime.Now, Order = order }); 

            Console.Read();
        }
    }
}
