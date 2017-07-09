﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.Cognitive.EventBus
{
    /// <summary>
    /// 定义事件处理器公共接口，所有的事件处理都要实现该接口
    /// </summary>
    /// <typeparam name="TEventData"></typeparam>
    public interface IEventHandler<in TEventData> where TEventData : IEventData
    {
        /// <summary>
        /// 事件处理器实现该方法来处理事件
        /// </summary>
        /// <param name="eventData"></param>
        void HandleEvent(TEventData eventData);
    }
}
