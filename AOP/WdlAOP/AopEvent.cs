using System;
using System.Reflection;

namespace WdlProxyAOP
{
    public class AopEvent<T>
    {
        #region 单例模式
        private static readonly object locked = new object();
        private static AopEvent<T> _Instance = new AopEvent<T>();
        public static AopEvent<T> Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (locked)
                    {
                        if (_Instance == null)
                        {
                            _Instance = new AopEvent<T>();
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
        public event Action<T, MethodInfo> BeForeExecuted;
        public event Action<T, MethodInfo, object> AfterExecuted;
        public event Action<T, MethodInfo, Exception> ExceptionExecuted;

        internal static void InvokeBeForeExecuted(T instance, MethodInfo methodInfo)
        {
            Instance.BeForeExecuted?.Invoke(instance, methodInfo);
        }
        internal static void InvokeAfterExecuted(T instance, MethodInfo methodInfo, object result)
        {
            Instance.AfterExecuted?.Invoke(instance, methodInfo, result);
        }
        internal static void InvokeExceptionExecuted(T instance, MethodInfo methodInfo, Exception ex)
        {
            Instance.ExceptionExecuted?.Invoke(instance, methodInfo, ex);
        }

        public static void SubscribeAopBeFore(Action<T, MethodInfo> action)
        {
            Instance.BeForeExecuted += action;
        }
        public static void SubscribeAopAfter(Action<T, MethodInfo, object> action)
        {
            Instance.AfterExecuted += action;
        }
        public static void SubscribeAopException(Action<T, MethodInfo, Exception> action)
        {
            Instance.ExceptionExecuted += action;
        }
        public static void UnSubscribeAopBeFore(Action<T, MethodInfo> action)
        {
            Instance.BeForeExecuted -= action;
        }
        public static void UnbscribeAopAfter(Action<T, MethodInfo, object> action)
        {
            Instance.AfterExecuted -= action;
        }
        public static void UnbscribeAopException(Action<T, MethodInfo, Exception> action)
        {
            Instance.ExceptionExecuted -= action;
        }
        public static void UnbscribeAll()
        {
            Instance.BeForeExecuted = null;
            Instance.AfterExecuted = null;
            Instance.ExceptionExecuted = null;
        }
    }
}
