using System;
using System.Reflection;

namespace WdlProxyAOP
{
    public class ActionErrorFilter : FilterAttribute
    {
        internal override FilterType FilterType => FilterType.EXCEPTION;

        internal override void Execute<T>(T instance, MethodInfo methodInfo, object result)
        {
            AopEvent<T>.InvokeExceptionExecuted(instance, methodInfo, result as Exception);
        }
    }
}
