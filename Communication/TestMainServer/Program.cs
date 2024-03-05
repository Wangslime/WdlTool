using WdlSocketTcp;

namespace TestMainServer
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");


            #region WebSocketServer
            //WebSocketServer webSocketServer = new WebSocketServer();
            //webSocketServer.ConnictEvent += OnConnictEvent;
            //webSocketServer.ReceiveEventMsg += OnReceiveEventMsg;
            //webSocketServer.Start(3000);
            #endregion

            #region TcpServer
            SocketsTcpServer socketsTcpServer = new SocketsTcpServer(30000);
            socketsTcpServer.ReceiveClientMsg += SocketsTcpServer_ReceiveClientMsg;

            while (true) 
            {
                await Task.Delay(1000);
            }
            #endregion
        }

        private static string SocketsTcpServer_ReceiveClientMsg(string arg)
        {
            Console.WriteLine(arg);
            return "1";
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