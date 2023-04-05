using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace WdlNetIoc
{
    public class IocContainer
    {
        #region 单例模式
        private static readonly object locked = new object();
        private static IocContainer _Instance = null;
        private static IocContainer Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (locked)
                    {
                        if (_Instance == null)
                        {
                            _Instance = new IocContainer();
                        }
                    }
                }
                return _Instance;
            }
            set
            {
                lock (locked)
                {
                    _Instance = value;
                }
            }
        }
        #endregion

        public static IocContainer CreateIocContainer()
        { 
            return Instance;
        }
        private IocContainer() { }

        private ConcurrentDictionary<string, ServiceDescriptor> dicIocContainer = new ConcurrentDictionary<string, ServiceDescriptor>();

        internal void AddScoped(Type type)
        {
            ServiceDescriptor serviceDescriptor = new ServiceDescriptor(type, type, ServiceLifetime.Scoped);
            if (dicIocContainer.ContainsKey(type.FullName))
            {
                dicIocContainer[type.FullName] = serviceDescriptor;
            }
            else
            {
                dicIocContainer.TryAdd(type.FullName, serviceDescriptor);
            }
            
        }
        internal void AddScoped(Type typeKey, Type typeValue)
        {
            ServiceDescriptor serviceDescriptor = new ServiceDescriptor(typeKey, typeValue, ServiceLifetime.Scoped);
            if (dicIocContainer.ContainsKey(typeKey.FullName))
            {
                dicIocContainer[typeKey.FullName] = serviceDescriptor;
            }
            else
            {
                dicIocContainer.TryAdd(typeKey.FullName, serviceDescriptor);
            }
        }

        internal void AddSingleton(Type type)
        {
            ServiceDescriptor serviceDescriptor = new ServiceDescriptor(type, type, ServiceLifetime.Scoped);
            if (dicIocContainer.ContainsKey(type.FullName))
            {
                dicIocContainer[type.FullName] = serviceDescriptor;
            }
            else
            {
                dicIocContainer.TryAdd(type.FullName, serviceDescriptor);
            }
        }
        internal void AddSingleton(Type typeKey, Type typeValue)
        {
            ServiceDescriptor serviceDescriptor = new ServiceDescriptor(typeKey, typeValue, ServiceLifetime.Scoped);
            if (dicIocContainer.ContainsKey(typeKey.FullName))
            {
                dicIocContainer[typeKey.FullName] = serviceDescriptor;
            }
            else
            {
                dicIocContainer.TryAdd(typeKey.FullName, serviceDescriptor);
            }
        }


        internal object IocGetService(Type type)
        {
            return PrivateGetService(type);
        }

        private object PrivateGetService(Type type)
        {
            object retObj = null;
            #region 构造函数注入
            {
                ConstructorInfo[] constructorInfos = type.GetConstructors();
                ConstructorInfo constructorInfo;// = constructorInfos?.MaxBy(p => p.GetParameters().Length);
                if (constructorInfos.Any(p => p.IsDefined(typeof(IocConstructorAttribute), true)))
                {
                    constructorInfo = constructorInfos?.FirstOrDefault(p => p.IsDefined(typeof(IocConstructorAttribute), true));
                }
                else
                {
                    constructorInfo = constructorInfos?.OrderByDescending(p=> p.GetParameters().Length).First();
                }
                List<object> paramsList = new List<object>();
                foreach (ParameterInfo parameterInfo in constructorInfo?.GetParameters())
                {
                    Type parameter = parameterInfo.ParameterType;
                    Type parameterType = dicIocContainer[parameter.FullName].TypeValue;
                    object paramerObj = PrivateGetService(parameterType);
                    paramsList.Add(paramerObj);
                }
                retObj = dicIocContainer[type.FullName].GetService(paramsList.ToArray());
            }
            #endregion

            #region 属性注入
            {
                foreach (PropertyInfo propertyInfo in type.GetProperties())
                {
                    Type property = propertyInfo.PropertyType;
                    if (dicIocContainer.ContainsKey(property.FullName))
                    {
                        Type propertyType = dicIocContainer[property.FullName].TypeValue;
                        object paramerObj = PrivateGetService(propertyType);
                        propertyInfo.SetValue(retObj, paramerObj, null);
                    }
                }
            }
            #endregion
            return retObj;
        }

        internal static T CreateDeepCopy<T>(T obj)
        {
            T t;
            MemoryStream memoryStream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(memoryStream, obj);
            memoryStream.Position = 0;
            t = (T)formatter.Deserialize(memoryStream);
            return t;
        }
    }
}