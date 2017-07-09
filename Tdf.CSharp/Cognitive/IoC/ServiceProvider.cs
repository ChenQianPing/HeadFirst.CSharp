using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.Cognitive.IoC
{
    /// <summary>
    /// 服务提供类的实现，类似服务工厂
    /// </summary>
    public class ServiceProvider : IServiceContainer
    {
        #region Fields
        /// <summary>
        /// 锁
        /// </summary>
        private readonly object _syncLock = new object();

        /// <summary>
        /// 存储单例工厂
        /// </summary>
        private readonly ConcurrentDictionary<Type, object> _factories = new ConcurrentDictionary<Type, object>();

        /// <summary>
        /// 存储注册类型
        /// </summary>
        private readonly ConcurrentDictionary<Type, Type> _registrations = new ConcurrentDictionary<Type, Type>();

        /// <summary>
        /// 存储实例
        /// </summary>
        private readonly ConcurrentDictionary<Type, object> _instances = new ConcurrentDictionary<Type, object>();
        #endregion

        #region 私有方法
        /// <summary>
        /// 判断服务是否已经被注册过
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        private bool ServiceIsRegistered(Type serviceType)
        {
            // 判断是否在工厂或者注册库内已经注册过该类型
            return _factories.ContainsKey(serviceType) || _registrations.ContainsKey(serviceType);
        }

        private object CreateServiceInstance(Type implementationType)
        {
            // 获取构造器
            var constructors = implementationType.GetConstructors();
            // 遍历构造器中的参数类型，获取参数
            var parameters = constructors[0]
                .GetParameters()
                .Select(parameterInfo => Resolve(parameterInfo.ParameterType))
                .ToArray();

            // 创建实例方法
            return constructors[0].Invoke(parameters);
        }

        private object Resolve(Type serviceType)
        {
            return GetType()
                .GetMethod("Resolve", new Type[0])
                .MakeGenericMethod(serviceType)
                .Invoke(this, new object[0]);
        }

        #endregion

        #region 接口实现 
        /// <summary>
        /// 注册工厂
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="serviceCreator"></param>
        /// <returns></returns>
        public IServiceRegister Register<TService>(Func<IServiceProvider, TService> serviceCreator) where TService : class
        {
            lock (_syncLock)
            {
                var serviceType = typeof(TService);
                if (ServiceIsRegistered(serviceType)) { return this; }

                // 将服务添加到存储器中
                _factories.TryAdd(serviceType, serviceCreator);
                return this;
            }
        }

        /// <summary>
        /// 注册实例类
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <returns></returns>
        public IServiceRegister Register<TService, TImplementation>() where TService : class where TImplementation : class, TService
        {
            lock (_syncLock)
            {
                var serviceType = typeof(TService);
                var implType = typeof(TImplementation);

                if (ServiceIsRegistered(serviceType)) { return this; }

                // 如果注册的类不是继承自TService接口，抛出异常
                if (!serviceType.IsAssignableFrom(implType))
                {
                    throw new Exception($"类型 {implType.Name} 不继承接口 {serviceType.Name}");
                }

                // 获取构造方法，必须只有一个构造方法（why？）
                var constructors = implType.GetConstructors();
                if (constructors.Length != 1)
                {
                    throw new Exception($"服务实例必须有且只有一个构造方法.当前实例 {implType.Name} 拥有 {constructors.Length.ToString()} 个");
                }

                // 添加
                _registrations.TryAdd(serviceType, implType);
                return this;
            }
        }

        public TService Resolve<TService>() where TService : class
        {
            var serviceType = typeof(TService);
            object service;
            // 如果实例存储器中已经存在该实例，直接返回
            if (_instances.TryGetValue(serviceType, out service))
                return (TService)service;

            lock (_syncLock)
            {
                // 加锁，再次判断
                if (_instances.TryGetValue(serviceType, out service))
                {
                    return (TService)service;
                }

                // 如果注册器中存在该类型，创建该实例，并加入到实例存储器中
                if (_registrations.ContainsKey(serviceType))
                {
                    var implementationType = _registrations[serviceType];
                    service = CreateServiceInstance(implementationType);
                    _instances.TryAdd(serviceType, service);
                }
                else if (_factories.ContainsKey(serviceType))
                {
                    service = ((Func<IServiceProvider, TService>)_factories[serviceType])(this);
                    _instances.TryAdd(serviceType, service);
                }
                else
                {
                    throw new Exception($"服务类型 {serviceType.Name} 未注册");
                }
                return (TService)service;
            }
        }
        #endregion


    }
}


/*
 * 几句代码简单实现IoC容器；
 * 思路就是通过动态调用构造函数生成对象， 然后将对象保存，调用的时候进行单例调用，
 * 而且，代码中不会存在 new 字眼。所有实例对象的创建和映射都在容器中实现。
 * 
 */
