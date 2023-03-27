using System.Reflection;
using WdlProxyAOP;

namespace TestController
{
    public class ActionExecutingFilter : FilterAttribute
    {
        public override string FilterType => "BEFORE";

        public override void Execute(object instance, MethodInfo methodInfo, object result)
        {
            Console.WriteLine($"我是在{methodInfo.Name}.ActionExecutingFilter中拦截发出的消息！-{DateTime.Now.ToString()}");
        }
    }
}
