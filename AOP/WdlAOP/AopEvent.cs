using System;
using System.Reflection;

namespace WdlProxyAOP
{
    public class AopEvent
    {
        #region 单例模式
        private static readonly object locked = new object();
        private static AopEvent _Instance = new AopEvent();
        public static AopEvent Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (locked)
                    {
                        if (Instance == null)
                        {
                            _Instance = new AopEvent();
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
        private event Action<object, MethodInfo> BeForeExecuted;
        private event Action<object, MethodInfo, object> AfterExecuted;
        private event Action<object, MethodInfo, Exception> ExceptionExecuted;

        internal static void InvokeBeForeExecuted(object instance, MethodInfo methodInfo)
        {
            Instance.BeForeExecuted?.Invoke(instance, methodInfo);
        }
        internal static void InvokeAfterExecuted(object instance, MethodInfo methodInfo, object result)
        {
            Instance.AfterExecuted?.Invoke(instance, methodInfo, result);
        }
        internal static void InvokeExceptionExecuted(object instance, MethodInfo methodInfo, Exception ex)
        {
            Instance.ExceptionExecuted?.Invoke(instance, methodInfo, ex);
        }

        public static void SubscribeAopBeFore(Action<object, MethodInfo> action)
        {
            Instance.BeForeExecuted += action;
        }
        public static void SubscribeAopAfter(Action<object, MethodInfo, object> action)
        {
            Instance.AfterExecuted += action;
        }
        public static void SubscribeAopException(Action<object, MethodInfo, Exception> action)
        {
            Instance.ExceptionExecuted += action;
        }
    }
}
