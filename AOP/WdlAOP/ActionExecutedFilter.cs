﻿using System.Reflection;

namespace WdlProxyAOP
{
    public class ActionExecutedFilter : FilterAttribute
    {
        internal override FilterType FilterType => FilterType.AFTER;

        internal override void Execute<T>(T instance, MethodInfo methodInfo, object result)
        {
            AopEvent<T>.InvokeAfterExecuted(instance, methodInfo, result);
            //Console.WriteLine($"我是在{methodInfo.Name}.ActionExecutedFilter中拦截发出的消息！-{DateTime.Now.ToString()}");
        }
    }
}
