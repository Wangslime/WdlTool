using System;
using System.Reflection;

namespace Drsoft.Plugin.ProxyAop
{
    public class ActionErrorFilter : FilterAttribute
    {
        internal override FilterType FilterType => FilterType.EXCEPTION;

        internal override void Execute<T>(T instance, MethodInfo methodInfo, object result, double time, params object[]? param)
        {
            AopEvent<T>.InvokeExceptionExecuted(instance, methodInfo, result as Exception, time, param);
        }
    }
}
