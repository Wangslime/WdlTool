using WdlSerialPort;

namespace TestSerialConsoleApp2
{
    internal class Program
    {
        static SerialPortClient serialPortClient = new SerialPortClient();
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            serialPortClient.ReceiveMsgEvent += SerialPortClient_ReceiveMsgEvent;
            serialPortClient.Start("COM201", 9600);
            while (true)
            {
                Thread.Sleep(1000);
            }
        }

        private static string SerialPortClient_ReceiveMsgEvent(string obj)
        {
            Console.WriteLine(obj);
            string ret = "";

            return ret;
        }
    }
}
