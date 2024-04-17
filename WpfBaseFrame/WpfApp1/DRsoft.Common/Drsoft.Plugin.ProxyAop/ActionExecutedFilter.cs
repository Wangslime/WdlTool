using System.Reflection;

namespace Drsoft.Plugin.ProxyAop
{
    public class ActionExecutedFilter : FilterAttribute
    {
        internal override FilterType FilterType => FilterType.AFTER;

        internal override void Execute<T>(T instance, MethodInfo methodInfo, object result, double time, params object[]? param)
        {
            AopEvent<T>.InvokeAfterExecuted(instance, methodInfo, result, time, param);
        }
    }
}