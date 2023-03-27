using WdlReflectionAOP;

namespace TestController
{
    internal class Program
    {
        static void Main(string[] args)
        {
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