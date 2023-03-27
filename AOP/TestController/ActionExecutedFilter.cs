using System.Reflection;
using WdlReflectionAOP;

namespace TestController
{
    public class ActionExecutedFilter : FilterAttribute
    {
        public override string FilterType => "AFTER";

        public override void Execute(object instance, MethodInfo methodInfo, object result)
        {
            Console.WriteLine($"我是在{methodInfo.Name}.ActionExecutedFilter中拦截发出的消息！-{DateTime.Now.ToString()}");
        }
    }
}
