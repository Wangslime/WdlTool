using System;
using System.Reflection;

namespace WdlProxyAOP
{
    public abstract class FilterAttribute : Attribute
    {
        public abstract string FilterType { get; }
        public abstract void Execute(object instance, MethodInfo methodInfo, object result);
    }

}
