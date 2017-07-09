using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.Cognitive.EventBus.Chapter2
{
    public class OrderEntity
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }
        
        /// <summary>
        /// 下单日期
        /// </summary>
        public DateTime OrderDateTime { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal OrderAmount { get; set; }
    }
}
