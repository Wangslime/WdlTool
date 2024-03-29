using System.Collections.Concurrent;
using System.Text;

namespace TestMainClient
{
    internal class Program
    {
        static ConcurrentQueue<string> queuePlcAdaptor = new ConcurrentQueue<string>();
        static void Main(string[] args)
        {
          

            NlogTest nlogTest = new NlogTest();
            nlogTest.Start();

            //BeckhoffPlcData beckhoffPlcData = new BeckhoffPlcData();
            //Task.Run(async () => { await beckhoffPlcData.Initial(); });

            while (true) {Thread.Sleep(1000);}
            #region WebSocketClient
            //WebSocketClient webSocketClient = new WebSocketClient();
            //webSocketClient.ReceiveEventMsg += OnReceiveEventMsg;
            //webSocketClient.Start("ws://192.168.3.21:3000");
            //while (true)
            //{
            //    Console.ReadKey();
            //    webSocketClient.Send("客户端发送数据");
            //}
            #endregion

            #region TCP客户端长连接
            //AsyncTcpClient tcpClient = new AsyncTcpClient("127.0.0.1", "9502");
            //tcpClient.EventDataReceived += ReadReceive;
            //tcpClient.EventServerConnected += ConnectTcp;
            //tcpClient.logError += LogError;
            //tcpClient.ReconnectTimeOut = 5000;
            //tcpClient.Start();
            //tcpClient.Send("我是测试数据");
            #endregion

            #region TCP客户端短链接
            //await AsyncTcpClient.ShortStartAsync("127.0.0.1", 9502, "我是测试数据");
            #endregion
        }

        private static string OnReceiveEventMsg(string arg)
        {
            Console.WriteLine(arg);
            return "";
        }



        #region TCP客户端长连接
        private static void ReadReceive(byte[] plcRead)
        {
            string strPlcRead = UTF8Encoding.UTF8.GetString(plcRead);
            queuePlcAdaptor.Enqueue(strPlcRead);
        }

        private static void ConnectTcp(string msg, bool isConnected)
        {
            if (isConnected)
            {
                Console.WriteLine($"{msg}：连接成功！");
            }
            else
            {
                Console.WriteLine($"{msg}：连接失败！");
            }
        }

        private static void LogError(string obj)
        {
            Console.WriteLine(obj);
        }
        #endregion
    }
}