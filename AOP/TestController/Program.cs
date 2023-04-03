using System.Reflection;
using WdlProxyAOP;

namespace TestController
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //new AopExecuted();
            //AopEvent.SubscribeAopBeFore(OnAopBeFore);
            //AopEvent.SubscribeAopAfter(OnAopAfter);
            //AopEvent.SubscribeAopException(OnException);

            AopEvent<ClassInterface>.Instance.BeForeExecuted += OnAopBeFore;
            AopEvent<ClassInterface>.Instance.AfterExecuted += OnAopAfter;
            AopEvent<ClassInterface>.Instance.ExceptionExecuted += OnException;
            try
            {
                ClassInterface Class1 = new Class1();
                Class1 = ProxyFactory.Creat(Class1);
                Class1.Add();
                Class1.TestError();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"发生错误：{ex.Message}");
                Console.ResetColor();
            }

            Console.ReadKey();
        }

        private static void OnAopBeFore(ClassInterface arg1, MethodInfo arg2)
        {
            
        }

        private static void OnAopAfter(ClassInterface arg1, MethodInfo arg2, object arg3)
        {
            
        }

        private static void OnException(ClassInterface arg1, MethodInfo arg2, Exception arg3)
        {
            
        }
    }

    public class AopExecuted
    {
        public AopExecuted()
        {
            AopEvent<ClassInterface>.SubscribeAopBeFore(OnAopBeFore);
            AopEvent<ClassInterface>.SubscribeAopAfter(OnAopAfter);
            AopEvent<ClassInterface>.SubscribeAopException(OnException);
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
    public class Class1: ClassInterface
    {
        public int Add()
        {
            return 1;
        }

        [ActionExecutedFilter]
        [ActionExecutingFilter]
        public void TestError()
        { 
            throw new Exception("TestError");
        }
    }
}