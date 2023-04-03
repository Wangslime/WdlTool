using System.Reflection;

namespace WdlProxyAOP
{
    public class ActionExecutingFilter : FilterAttribute
    {
        internal override FilterType FilterType => FilterType.BEFORE;

        internal override void Execute<T>(T instance, MethodInfo methodInfo, object result)
        {
            AopEvent<T>.InvokeBeForeExecuted(instance, methodInfo);
        }
    }
}