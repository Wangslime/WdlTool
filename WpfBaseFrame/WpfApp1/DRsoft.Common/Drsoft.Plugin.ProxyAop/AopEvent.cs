using System;
using System.Reflection;

namespace Drsoft.Plugin.ProxyAop
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
        public event Action<T, MethodInfo, double, object[]?> BeForeExecuted;
        public event Action<T, MethodInfo, object, double, object[]?> AfterExecuted;
        public event Action<T, MethodInfo, Exception, double, object[]?> ExceptionExecuted;

        internal static void InvokeBeForeExecuted(T instance, MethodInfo methodInfo, double time, params object[]? parObjs)
        {
            Instance.BeForeExecuted?.Invoke(instance, methodInfo, time, parObjs);
        }
        internal static void InvokeAfterExecuted(T instance, MethodInfo methodInfo, object result, double time, params object[]? parObjs)
        {
            Instance.AfterExecuted?.Invoke(instance, methodInfo, result, time, parObjs);
        }
        internal static void InvokeExceptionExecuted(T instance, MethodInfo methodInfo, Exception ex, double time, params object[]? parObjs)
        {
            Instance.ExceptionExecuted?.Invoke(instance, methodInfo, ex, time, parObjs);
        }
 
        public static void UnbscribeAll()
        {
            Instance.BeForeExecuted = null;
            Instance.AfterExecuted = null;
            Instance.ExceptionExecuted = null;
        }
    }
}
