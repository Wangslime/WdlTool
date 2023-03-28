using System;
using System.Reflection;

namespace WdlProxyAOP
{
    public abstract class FilterAttribute : Attribute
    {
        internal abstract FilterType FilterType { get; }
        internal abstract void Execute(object instance, MethodInfo methodInfo, object result);
    }

}
