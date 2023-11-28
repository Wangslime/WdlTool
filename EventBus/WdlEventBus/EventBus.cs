using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WdlEventBus
{
    public class EventBus
    {
        #region 单例模式
        private static readonly object locked = new object();
        private static EventBus _Instance = new EventBus();
        public static EventBus Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (locked)
                    {
                        if (_Instance == null)
                        {
                            _Instance = new EventBus();
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

        #region 泛型事件订阅执行集合
        /// <summary>
        /// 泛型事件集合
        /// </summary>
        private ConcurrentDictionary<Type, List<IEventData>> dicEventType = new ConcurrentDictionary<Type, List<IEventData>>();

        #region 订阅

        /// <summary>
        /// 同步订阅不带返回值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        public static void Subscribe<T>(Action<EventData<T>> action)
        {
            IEventData eventHander = new EventHander<T>(action, null);
            Instance.PrivateSubscribe<T>(eventHander);
        }

        /// <summary>
        /// 同步订阅带返回值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        public static void SubscribeResult<T>(Func<EventData<T>, object> func)
        {
            IEventData eventHander = new EventHander<T>(null, func);
            Instance.PrivateSubscribe<T>(eventHander);
        }

        /// <summary>
        /// 异步订阅不返回值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        public static void SubscribeAsync<T>(Func<EventData<T>, Task> func)
        {
            IEventData eventHander = new EventHanderAsync<T>(func, null);
            Instance.PrivateSubscribe<T>(eventHander);
        }

        /// <summary>
        /// 异步订阅带返回值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        public static void SubscribeResultAsync<T>(Func<EventData<T>, Task<object>> func)
        {
            IEventData eventHander = new EventHanderAsync<T>(null, func);
            Instance.PrivateSubscribe<T>(eventHander);
        }

        private void PrivateSubscribe<T>(IEventData eventData)
        {
            Type type = typeof(T);
            if (!dicEventType.ContainsKey(type))
            {
                dicEventType.TryAdd(type, new List<IEventData>());
            }
            if (dicEventType.TryGetValue(type, out var actionList))
            {
                if (actionList == null)
                {
                    actionList = new List<IEventData>();
                }
                actionList.Add(eventData);
            }
            else
            {
                throw new Exception($"订阅事件添加失败");
            }
        }
        #endregion

        #region 执行

        /// <summary>
        /// 同步执行不带返回值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventData"></param>
        public static void Publish<T>()
        {
            EventData<T> eventData = new EventData<T>();
            Publish(eventData);
        }
        public static void Publish<T>(string eventName)
        {
            EventData<T> eventData = new EventData<T>() {EventName = eventName };
            Publish(eventData);
        }
        public static void Publish<T>(T t)
        {
            EventData<T> eventData = new EventData<T>() { Data = t };
            Publish(eventData);
        }
        public static void Publish<T>(string eventName, T t)
        {
            EventData<T> eventData = new EventData<T>() { EventName = eventName, Data = t };
            Publish(eventData);
        }
        public static void Publish<T>(string methodName, string eventName)
        {
            EventData<T> eventData = new EventData<T>() { MethodName = methodName, EventName = eventName };
            Publish(eventData);
        }
        public static void Publish<T>(string methodName, string eventName, T t)
        {
            EventData<T> eventData = new EventData<T>() { MethodName = methodName, EventName = eventName, Data = t };
            Publish(eventData);
        }
        public static void Publish<T>(EventData<T> eventData)
        {
            Instance.PrivatePublish(eventData);
        }
        private void PrivatePublish<T>(EventData<T> eventData)
        {
            Type type = typeof(T);
            if (dicEventType.ContainsKey(type))
            {
                if (dicEventType.TryGetValue(type, out var actionList))
                {
                    if (actionList != null && actionList.Any())
                    {
                        foreach (var item in actionList)
                        {
                            if (item is EventHander<T>)
                            {
                                EventHander<T> eventHander = item as EventHander<T>;

                                if (eventHander != null && eventHander.action != null && (string.IsNullOrEmpty(eventData.MethodName) || eventHander.action.Method.Name == eventData.MethodName))
                                {
                                    eventHander.HandleEventAction(eventData);
                                }
                            }
                        }
                    }
                }
            }
        }


        /// <summary>
        /// 同步执行带返回值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventData"></param>
        /// <returns></returns>
        public static List<object> PublishResult<T>()
        {
            EventData<T> eventData = new EventData<T>();
            return PublishResult(eventData);
        }
        public static List<object> PublishResult<T>(string eventName)
        {
            EventData<T> eventData = new EventData<T>() { EventName = eventName };
            return PublishResult(eventData);
        }
        public static List<object> PublishResult<T>(T t)
        {
            EventData<T> eventData = new EventData<T>() { Data = t };
            return PublishResult(eventData);
        }
        public static List<object> PublishResult<T>(string eventName, T t)
        {
            EventData<T> eventData = new EventData<T>() { EventName = eventName, Data = t };
            return PublishResult(eventData);
        }
        public static List<object> PublishResult<T>(string methodName, string eventName)
        {
            EventData<T> eventData = new EventData<T>() { MethodName = methodName, EventName = eventName };
            return PublishResult(eventData);
        }
        public static List<object> PublishResult<T>(string methodName, string eventName, T t)
        {
            EventData<T> eventData = new EventData<T>() { MethodName = methodName, EventName = eventName, Data = t };
            return PublishResult(eventData);
        }
        public static List<object> PublishResult<T>(EventData<T> eventData)
        {
            return Instance.PrivatePublishResult(eventData);
        }
        private List<object> PrivatePublishResult<T>(EventData<T> eventData)
        {
            List<object> list = new List<object>();
            Type type = typeof(T);
            if (dicEventType.ContainsKey(type))
            {
                if (dicEventType.TryGetValue(type, out var actionList))
                {
                    if (actionList != null && actionList.Any())
                    {
                        foreach (var item in actionList)
                        {
                            if (item is EventHander<T>)
                            {
                                EventHander<T> eventHander = item as EventHander<T>;
                                if (eventHander != null && eventHander.func != null && (string.IsNullOrEmpty(eventData.MethodName) || eventHander.func.Method.Name == eventData.MethodName))
                                {
                                    object obj = eventHander.HandleEventFunc(eventData);
                                    list.Add(obj);
                                }
                            }
                        }
                    }
                }
            }
            return list;
        }


        /// <summary>
        /// 异步执行不带返回值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventData"></param>
        /// <returns></returns>
        public static Task PublishAsync<T>()
        {
            EventData<T> eventData = new EventData<T>();
            return PublishAsync(eventData);
        }
        public static Task PublishAsync<T>(string eventName)
        {
            EventData<T> eventData = new EventData<T>() { EventName = eventName };
            return PublishAsync(eventData);
        }
        public static Task PublishAsync<T>(T t)
        {
            EventData<T> eventData = new EventData<T>() { Data = t };
            return PublishAsync(eventData);
        }
        public static Task PublishAsync<T>(string eventName, T t)
        {
            EventData<T> eventData = new EventData<T>() { EventName = eventName, Data = t };
            return PublishAsync(eventData);
        }
        public static Task PublishAsync<T>(string methodName, string eventName)
        {
            EventData<T> eventData = new EventData<T>() { MethodName = methodName, EventName = eventName};
            return PublishAsync(eventData);
        }
        public static Task PublishAsync<T>(string methodName, string eventName, T t)
        {
            EventData<T> eventData = new EventData<T>() { MethodName = methodName, EventName = eventName, Data = t };
            return PublishAsync(eventData);
        }
        public static Task PublishAsync<T>(EventData<T> eventData)
        {
            return Instance.PrivatePublishAsync(eventData);
        }
        private Task PrivatePublishAsync<T>(EventData<T> eventData)
        {
            Task task = Task.Run(()=> true);
            Type type = typeof(T);
            if (dicEventType.ContainsKey(type))
            {
                if (dicEventType.TryGetValue(type, out var actionList))
                {
                    if (actionList != null && actionList.Any())
                    {
                        foreach (var item in actionList)
                        {
                            if (item is EventHanderAsync<T>)
                            {
                                EventHanderAsync<T> eventHander = item as EventHanderAsync<T>;

                                if (eventHander != null && eventHander.funcVoid != null && (string.IsNullOrEmpty(eventData.MethodName) || eventHander.funcVoid.Method.Name == eventData.MethodName))
                                {
                                    task = eventHander.HandleEventVoid(eventData);
                                }
                            }
                        }
                    }
                }
            }
            return task;
        }


        /// <summary>
        /// 异步执行带返回值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventData"></param>
        /// <returns></returns>
        public static Task PublishResultAsync<T>()
        {
            EventData<T> eventData = new EventData<T>();
            return PublishResultAsync(eventData);
        }
        public static Task PublishResultAsync<T>(string eventName)
        {
            EventData<T> eventData = new EventData<T>() { EventName = eventName };
            return PublishResultAsync(eventData);
        }
        public static Task PublishResultAsync<T>(T t)
        {
            EventData<T> eventData = new EventData<T>() { Data = t };
            return PublishResultAsync(eventData);
        }
        public static Task PublishResultAsync<T>(string methodName, T t)
        {
            EventData<T> eventData = new EventData<T>() { MethodName = methodName, Data = t };
            return PublishResultAsync(eventData);
        }
        public static Task PublishResultAsync<T>(string methodName, string eventName)
        {
            EventData<T> eventData = new EventData<T>() { MethodName = methodName, EventName = eventName };
            return PublishResultAsync(eventData);
        }
        public static Task PublishResultAsync<T>(string methodName, string eventName, T t)
        {
            EventData<T> eventData = new EventData<T>() { MethodName = methodName, EventName = eventName, Data = t };
            return PublishResultAsync(eventData);
        }
        public static Task<List<object>> PublishResultAsync<T>(EventData<T> eventData)
        {
            return Instance.PrivatePublishResultAsync(eventData);
        }
        private async Task<List<object>> PrivatePublishResultAsync<T>(EventData<T> eventData)
        {
            List<object> list = new List<object>();
            Type type = typeof(T);
            if (dicEventType.ContainsKey(type))
            {
                if (dicEventType.TryGetValue(type, out var actionList))
                {
                    if (actionList != null && actionList.Any())
                    {
                        foreach (var item in actionList)
                        {
                            if (item is EventHanderAsync<T>)
                            {
                                EventHanderAsync<T> eventHander = item as EventHanderAsync<T>;
                                if (eventHander != null && eventHander.func != null && (string.IsNullOrEmpty(eventData.MethodName) || eventHander.func.Method.Name == eventData.MethodName))
                                {
                                    object obj = await eventHander.HandleEventObj(eventData);
                                    list.Add(obj);
                                }
                            }
                        }
                    }
                }
            }
            return list;
        }
        #endregion

        #endregion

        #region 函数名称订阅执行集合

        private ConcurrentDictionary<string, List<IEventData>> dicEventMethod = new ConcurrentDictionary<string, List<IEventData>>();

        #region 订阅

        /// <summary>
        /// 同步订阅无返回值
        /// </summary>
        /// <param name="action"></param>
        public static void Subscribe(Action<EventData> action)
        {
            if (action != null && action.Method != null && !string.IsNullOrEmpty(action.Method.Name))
            {
                IEventData eventHander = new EventHander(action, null);
                Instance.PrivateSubscribe(action.Method.Name, eventHander);
            }
        }

        /// <summary>
        /// 同步订阅带返回值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        public static void SubscribeResult(Func<EventData, object> func)
        {
            if (func != null && func.Method != null && !string.IsNullOrEmpty(func.Method.Name))
            {
                IEventData eventHander = new EventHander(null, func);
                Instance.PrivateSubscribe(func.Method.Name, eventHander);
            }
        }

        /// <summary>
        /// 异步订阅不返回值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        public static void SubscribeAsync(Func<EventData, Task> func)
        {
            if (func != null && func.Method != null && !string.IsNullOrEmpty(func.Method.Name))
            {
                IEventData eventHander = new EventHanderAsync(func, null);
                Instance.PrivateSubscribe(func.Method.Name, eventHander);
            }
        }

        /// <summary>
        /// 异步订阅带返回值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        public static void SubscribeResultAsync(Func<EventData, Task<object>> func)
        {
            if (func != null && func.Method != null && !string.IsNullOrEmpty(func.Method.Name))
            {
                IEventData eventHander = new EventHanderAsync(null, func);
                Instance.PrivateSubscribe(func.Method.Name, eventHander);
            }
        }

        private void PrivateSubscribe(string methodName, IEventData eventHander)
        {
            if (!dicEventMethod.ContainsKey(methodName))
            {
                dicEventMethod[methodName] = new List<IEventData>();
            }
            if (dicEventMethod.TryGetValue(methodName, out var eventDatas))
            {
                if (eventDatas != null)
                {
                    eventDatas.Add(eventHander);
                }
            }
        }

        #endregion

        #region 执行

        /// <summary>
        /// 同步执行不带返回值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventData"></param>
        public static void Publish(string methodName)
        {
            EventData eventData = new EventData() { MethodName = methodName };
            Publish(eventData);
        }
        public static void Publish(string methodName, string eventName)
        {
            EventData eventData = new EventData() { EventName = eventName, MethodName = methodName };
            Publish(eventData);
        }
        public static void Publish(string methodName, object data)
        {
            EventData eventData = new EventData() { MethodName = methodName, Data = data };
            Publish(eventData);
        }
        public static void Publish(string methodName, string eventName,  object data)
        {
            EventData eventData = new EventData() { EventName = eventName, MethodName = methodName, Data = data };
            Publish(eventData);
        }
        public static void Publish(EventData eventData)
        {
            Instance.PrivatePublish(eventData);
        }
        private void PrivatePublish(EventData eventData)
        {
            if (eventData != null && !string.IsNullOrEmpty(eventData.MethodName) && dicEventMethod.ContainsKey(eventData.MethodName))
            {
                if (dicEventMethod.TryGetValue(eventData.MethodName, out var eventDatas))
                {
                    if (eventDatas != null && eventDatas.Any())
                    {
                        foreach (var item in eventDatas)
                        {
                            if (item is EventHander)
                            {
                                EventHander eventHander = item as EventHander;

                                if (eventHander != null && eventHander.action != null && (string.IsNullOrEmpty(eventData.MethodName) || eventHander.action.Method.Name == eventData.MethodName))
                                {
                                    eventHander.HandleEventViod(eventData);
                                }
                            }
                        }
                    }
                }
            }
        }


        /// <summary>
        /// 同步执行带返回值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventData"></param>
        /// <returns></returns>
        public static void PublishResult(string methodName)
        {
            EventData eventData = new EventData() { MethodName = methodName };
            PublishResult(eventData);
        }
        public static void PublishResult(string methodName, object data)
        {
            EventData eventData = new EventData() { MethodName = methodName, Data = data };
            PublishResult(eventData);
        }
        public static void PublishResult(string methodName, string eventName)
        {
            EventData eventData = new EventData() { MethodName = methodName, EventName = eventName };
            PublishResult(eventData);
        }
        public static void PublishResult(string methodName, string eventName, object data)
        {
            EventData eventData = new EventData() { MethodName = methodName, EventName = eventName, Data = data };
            PublishResult(eventData);
        }
        public static List<object> PublishResult(EventData eventData)
        {
            return Instance.PrivatePublishResult(eventData);
        }
        private List<object> PrivatePublishResult(EventData eventData)
        {
            List<object> list = new List<object>();
            if (eventData != null && !string.IsNullOrEmpty(eventData.MethodName) && dicEventMethod.ContainsKey(eventData.MethodName))
            {
                if (dicEventMethod.TryGetValue(eventData.MethodName, out var eventDatas))
                {
                    if (eventDatas != null && eventDatas.Any())
                    {
                        foreach (var item in eventDatas)
                        {
                            if (item is EventHander)
                            {
                                EventHander eventHander = item as EventHander;
                                if (eventHander != null && eventHander.func != null && (string.IsNullOrEmpty(eventData.MethodName) || eventHander.func.Method.Name == eventData.MethodName))
                                {
                                    object obj = eventHander.HandleEventObj(eventData);
                                    list.Add(obj);
                                }
                            }
                        }
                    }
                }
            }
            return list;
        }


        /// <summary>
        /// 异步执行不带返回值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventData"></param>
        /// <returns></returns>
        public static Task PublishAsync(string methodName)
        {
            EventData eventData = new EventData() { MethodName = methodName };
            return PublishAsync(eventData);
        }
        public static Task PublishAsync(string methodName, object data)
        {
            EventData eventData = new EventData() { MethodName = methodName, Data = data };
            return PublishAsync(eventData);
        }
        public static Task PublishAsync(string methodName, string eventName)
        {
            EventData eventData = new EventData() { MethodName = methodName, EventName = eventName };
            return PublishAsync(eventData);
        }
        public static Task PublishAsync(string methodName, string eventName, object data)
        {
            EventData eventData = new EventData() { MethodName = methodName, EventName = eventName, Data = data };
            return PublishAsync(eventData);
        }
        public static Task PublishAsync(EventData eventData)
        {
            return Instance.PrivatePublishAsync(eventData);
        }
        private async Task PrivatePublishAsync(EventData eventData)
        {
            if (eventData != null && !string.IsNullOrEmpty(eventData.MethodName) && dicEventMethod.ContainsKey(eventData.MethodName))
            {
                if (dicEventMethod.TryGetValue(eventData.MethodName, out var eventDatas))
                {
                    if (eventDatas != null && eventDatas.Any())
                    {
                        foreach (var item in eventDatas)
                        {
                            if (item is EventHanderAsync)
                            {
                                EventHanderAsync eventHander = item as EventHanderAsync;

                                if (eventHander != null && eventHander.funcVoid != null && (string.IsNullOrEmpty(eventData.MethodName) || eventHander.funcVoid.Method.Name == eventData.MethodName))
                                {
                                    await eventHander.HandleEventVoid(eventData);
                                }
                            }
                        }
                    }
                }
            }
        }


        /// <summary>
        /// 异步执行带返回值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventData"></param>
        /// <returns></returns>
        public static Task<List<object>> PublishResultAsync(string methodName)
        {
            EventData eventData = new EventData() { MethodName = methodName };
            return PublishResultAsync(eventData);
        }
        public static Task<List<object>> PublishResultAsync(string methodName, object data)
        {
            EventData eventData = new EventData() { MethodName = methodName, Data = data };
            return PublishResultAsync(eventData);
        }
        public static Task<List<object>> PublishResultAsync(string methodName, string eventName)
        {
            EventData eventData = new EventData() { MethodName = methodName, EventName = eventName };
            return PublishResultAsync(eventData);
        }
        public static Task<List<object>> PublishResultAsync(string methodName, string eventName, object data)
        {
            EventData eventData = new EventData() { MethodName = methodName, EventName = eventName, Data = data };
            return PublishResultAsync(eventData);
        }
        public static Task<List<object>> PublishResultAsync(EventData eventData)
        {
            return Instance.PrivatePublishResultAsync(eventData);
        }
        private async Task<List<object>> PrivatePublishResultAsync(EventData eventData)
        {
            List<object> list = new List<object>();
            if (eventData != null && !string.IsNullOrEmpty(eventData.MethodName) && dicEventMethod.ContainsKey(eventData.MethodName))
            {
                if (dicEventMethod.TryGetValue(eventData.MethodName, out var eventDatas))
                {
                    if (eventDatas != null && eventDatas.Any())
                    {
                        foreach (var item in eventDatas)
                        {
                            if (item is EventHanderAsync)
                            {
                                EventHanderAsync eventHander = item as EventHanderAsync;
                                if (eventHander != null && eventHander.func != null && (string.IsNullOrEmpty(eventData.MethodName) || eventHander.func.Method.Name == eventData.MethodName))
                                {
                                    object obj = await eventHander.HandleEventObj(eventData);
                                    list.Add(obj);
                                }
                            }
                        }
                    }
                }
            }
            return list;
        }

        #endregion

        #endregion

        #region 移除订阅
        public static void RemoveAll()
        {
            Instance.dicEventType.Clear();
            Instance.dicEventMethod.Clear();
        }

        public static void Remove<T>()
        {
            Type type = typeof(T);
            if (Instance.dicEventType.ContainsKey(type))
            {
                Instance.dicEventType.TryRemove(type, out _);
            }
        }

        public static void Remove<T>(string methodName)
        {
            Type type = typeof(T);
            if (Instance.dicEventType.ContainsKey(type))
            {
                if (Instance.dicEventType.TryGetValue(type, out var eventDatas))
                {
                    List<EventHander<T>> eventHanders = new List<EventHander<T>>();
                    List<EventHanderAsync<T>> eventHanderAsyncs = new List<EventHanderAsync<T>>();
                    foreach (var item in eventDatas)
                    {
                        if (item is EventHander<T>)
                        {
                            EventHander<T> eventHander = item as EventHander<T>;
                            if (eventHander != null)
                            {
                                if (eventHander.action != null && eventHander.action.Method.Name != methodName)
                                {
                                    eventHanders.Add(eventHander);
                                }
                                if (eventHander.func != null && eventHander.func.Method.Name != methodName)
                                {
                                    eventHanders.Add(eventHander);
                                }
                            }
                        }
                        else if (item is EventHanderAsync<T>)
                        {
                            EventHanderAsync<T> eventHander = item as EventHanderAsync<T>;
                            if (eventHander != null)
                            {
                                if (eventHander.funcVoid != null && eventHander.funcVoid.Method.Name != methodName)
                                {
                                    eventHanderAsyncs.Add(eventHander);
                                }
                                if (eventHander.func != null && eventHander.func.Method.Name != methodName)
                                {
                                    eventHanderAsyncs.Add(eventHander);
                                }
                            }
                        }
                    }
                    eventDatas.Clear();
                    if (eventHanders.Any())
                    {
                        foreach (var item in eventHanders)
                        {
                            eventDatas.Add(item);
                        }
                    }
                    if (eventHanderAsyncs.Any())
                    {
                        foreach (var item in eventHanderAsyncs)
                        {
                            eventDatas.Add(item);
                        }
                    }
                }
            }
        }

        public static void Remove(string methodName)
        {
            if (Instance.dicEventMethod.ContainsKey(methodName))
            {
                Instance.dicEventMethod.TryRemove(methodName, out _);
            }
        }
        #endregion
    }
}
