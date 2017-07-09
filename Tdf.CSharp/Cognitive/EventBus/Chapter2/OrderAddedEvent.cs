using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.Cognitive.EventBus.Chapter2
{
    public class OrderAddedEvent : EventData
    {
        public OrderEntity Order { get; set; }
    }
}
