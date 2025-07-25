﻿using System;
using System.Reflection;

namespace Long.ProxyAOP
{
    public class ActionErrorFilter : FilterAttribute
    {
        internal override FilterType FilterType => FilterType.EXCEPTION;

        internal override void Execute<T>(T instance, MethodInfo methodInfo, object result, double time, params object[] param)
        {
            AopEvent<T>.InvokeExceptionExecuted(instance, methodInfo, result as Exception, time, param);
        }
    }
}
