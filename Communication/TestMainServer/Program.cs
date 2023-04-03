using CommonCommunication;

namespace TestMainServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");


            #region WebSocketServer
            //WebSocketServer webSocketServer = new WebSocketServer();
            //webSocketServer.ConnictEvent += OnConnictEvent;
            //webSocketServer.ReceiveEventMsg += OnReceiveEventMsg;
            //webSocketServer.Start(3000);
            #endregion
        }

        private static string OnReceiveEventMsg(string arg)
        {
            Console.WriteLine(arg);
            return "已收到数据";
        }

        private static void OnConnictEvent(string obj)
        {
            Console.WriteLine(obj);
        }
    }
}