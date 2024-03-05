using WdlSerialPort;

namespace TestSerialConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            SerialPortClient serialPortClient = new SerialPortClient();
            serialPortClient.Start("COM200", 9600);
            while (true)
            {
                serialPortClient.SendMsg($"{DateTime.Now}---COM200");
                Thread.Sleep(1000);
            }
        }
    }
}
