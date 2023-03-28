using System.Reflection;

namespace WdlProxyAOP
{
    public class ActionExecutingFilter : FilterAttribute
    {
        internal override FilterType FilterType => FilterType.BEFORE;

        internal override void Execute(object instance, MethodInfo methodInfo, object result)
        {
            AopEvent.InvokeBeForeExecuted(instance, methodInfo);
        }
    }
}