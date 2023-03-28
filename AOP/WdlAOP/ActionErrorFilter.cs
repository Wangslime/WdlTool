using System;
using System.Reflection;

namespace WdlProxyAOP
{
    public class ActionErrorFilter : FilterAttribute
    {
        internal override FilterType FilterType => FilterType.EXCEPTION;

        internal override void Execute(object instance, MethodInfo methodInfo, object result)
        {
            AopEvent.InvokeExceptionExecuted(instance, methodInfo, result as Exception);
            //Console.WriteLine($"我是在{methodInfo.Name}.ActionErrorFilter中拦截发出的消息！-{DateTime.Now.ToString()}-Error Msg:{(result as Exception).Message}");
        }
    }
}
