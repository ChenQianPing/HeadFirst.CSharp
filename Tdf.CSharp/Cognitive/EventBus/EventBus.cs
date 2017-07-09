using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.Cognitive.EventBus
{
    /// <summary>
    /// 事件总线
    /// </summary>
    public class EventBus
    {
        private static EventBus _eventBus = null;

        /* 定义线程安全集合；
         * 在这个字典中，key存储的是事件，而value中存储的是事件处理程序；
         */
        private static readonly ConcurrentDictionary<Type, List<Type>> EventAndHandlerMapping = new ConcurrentDictionary<Type, List<Type>>();

        private EventBus() { }

        #region 单例
        /// <summary>
        /// 单例
        /// </summary>
        /// <returns></returns>
        public static EventBus Instance()
        {
            if (_eventBus == null)
            {
                _eventBus = new EventBus();
                MapEvent2Handler();
            }
            return _eventBus;
        }
        #endregion
        
        #region 发布
        /// <summary>
        /// 发布
        /// 这里没有用到队列之类的东西，使用的是直接调用的方式
        /// </summary>
        /// <param name="eventData"></param>
        public void Publish(EventData eventData)
        {
            // 找出这个事件对应的处理者
            var eventType = eventData.GetType();

            if (EventAndHandlerMapping.ContainsKey(eventType) == true)
            {
                foreach (var item in EventAndHandlerMapping[eventType])
                {
                    var mi = item.GetMethod("HandleEvent");
                    if (mi != null)
                    {
                        var o = Activator.CreateInstance(item);
                        mi.Invoke(o, new object[] { eventData });
                    }
                }
            }
        }
        #endregion

        #region 通过反射，将事件源与事件处理绑定
        /// <summary>
        /// 将事件与事件处理程序映射到一起
        /// 使用元数据来进行注册
        /// </summary>
        public static void MapEvent2Handler()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes();

            foreach (var type in types)
            {
                var handlerInterfaceType = type.GetInterface("IEventHandler`1");  // 事件处理者

                if (handlerInterfaceType != null) // 若是事件处理者，则以其泛型参数为key，事件处理者的集合为value添加到映射中
                {
                    var eventType = handlerInterfaceType.GetGenericArguments()[0]; // 这里只有一个
                    // 查找是否存在key
                    if (EventAndHandlerMapping.Keys.Contains(eventType))
                    {
                        var handlerTypes = EventAndHandlerMapping[eventType];
                        handlerTypes.Add(type);
                        EventAndHandlerMapping[eventType] = handlerTypes;
                    }
                    else // 存在则添加
                    {
                        var handlerTypes = new List<Type> {type};
                        EventAndHandlerMapping[eventType] = handlerTypes;
                    }
                }
            }
        }
        #endregion

    }
}

/*
 * 事件总线主要定义三个方法，注册、取消注册、事件触发。
 * 还有一点就是我们在构造函数中通过反射去进行事件源和事件处理的绑定。
 * 
 * 1.事件总线维护一个事件源与事件处理的映射字典；
 * 2.通过单例模式，确保事件总线的唯一入口；
 * 3.利用反射完成事件源与事件处理的初始化绑定；
 * 4.提供统一的事件注册、取消注册和触发接口。
 * 
 * 基本思路：
 * 
 * （1） 在事件总线内部维护着一个事件与事件处理程序相映射的字典。
 * （2） 利用反射，事件总线会将实现了IEventHandler的处理程序与相应事件关联到一起，相当于实现了事件处理程序对事件的订阅。
 * （3） 当发布事件时，事件总线会从字典中找出相应的事件处理程序，然后利用反射去调用事件处理程序中的方法。
 */
