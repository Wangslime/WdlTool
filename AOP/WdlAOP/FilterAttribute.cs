﻿using System.Reflection;

namespace Long.ProxyAOP
{
    public abstract class FilterAttribute : System.Attribute
    {
        internal abstract FilterType FilterType { get; }
        internal abstract void Execute<T>(T instance, MethodInfo methodInfo, object result, double time, params object[] param);
    }

}
