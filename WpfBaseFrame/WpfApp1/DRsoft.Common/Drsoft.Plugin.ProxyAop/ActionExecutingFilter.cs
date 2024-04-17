using System.Reflection;

namespace Drsoft.Plugin.ProxyAop
{
    public class ActionExecutingFilter : FilterAttribute
    {
        internal override FilterType FilterType => FilterType.BEFORE;

        internal override void Execute<T>(T instance, MethodInfo methodInfo, object result, double time, params object[]? param)
        {
            AopEvent<T>.InvokeBeForeExecuted(instance, methodInfo, time, param);
        }
    }
}