using System.Reflection;
using WdlProxyAOP;

namespace TestController
{
    public class ActionErrorFilter : FilterAttribute
    {
        public override string FilterType => "EXCEPTION";

        public override void Execute(object instance, MethodInfo methodInfo, object result)
        {
            Console.WriteLine($"我是在{methodInfo.Name}.ActionErrorFilter中拦截发出的消息！-{DateTime.Now.ToString()}-Error Msg:{(result as Exception).Message}");
        }
    }
}
