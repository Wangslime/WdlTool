using System.Reflection;
using WdlProxyAOP;

namespace TestController
{
    internal class Program
    {
        static void Main(string[] args)
        {
            new AopExecuted();
            AopEvent.SubscribeAopBeFore(OnAopBeFore);
            AopEvent.SubscribeAopAfter(OnAopAfter);
            AopEvent.SubscribeAopException(OnException);
            try
            {
                ClassInterface Class1 = new Class1();
                Class1 = ProxyFactory.Creat(Class1);
                Class1.Add();
                Class1.TestError();
            }
            catch (Exception ex)
            {
                ex.ToExString();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"发生错误：{ex.Message}");
                Console.ResetColor();
            }

            Console.ReadKey();
        }

        private static void OnException(object arg1, MethodInfo arg2, Exception arg3)
        {
            
        }

        private static void OnAopAfter(object arg1, MethodInfo arg2, object arg3)
        {
            
        }

        private static void OnAopBeFore(object arg1, MethodInfo arg2)
        {
            
        }
    }

    public class AopExecuted
    {
        public AopExecuted()
        {
            AopEvent.SubscribeAopBeFore(OnAopBeFore);
            AopEvent.SubscribeAopAfter(OnAopAfter);
            AopEvent.SubscribeAopException(OnException);
        }

        private void OnException(object arg1, MethodInfo arg2, Exception arg3)
        {
            
        }

        private void OnAopAfter(object arg1, MethodInfo arg2, object arg3)
        {
            
        }

        private void OnAopBeFore(object arg1, MethodInfo arg2)
        {
            
        }
    }

    public interface ClassInterface
    {
        int Add();
        void TestError();
    }

    [ActionErrorFilter]
    [ActionExecutedFilter]
    [ActionExecutingFilter]
    public class Class1: ClassInterface
    {
        public int Add()
        {
            return 1;
        }

        public void TestError()
        { 
            throw new Exception("TestError");
        }
    }
}