﻿using Long.RabbitMq;

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
            //SocketsTcpServer socketsTcpServer = new SocketsTcpServer(30000);
            //socketsTcpServer.ReceiveClientMsg += SocketsTcpServer_ReceiveClientMsg;

            //while (true) 
            //{
            //    await Task.Delay(1000);
            //}
            #endregion

            #region Rabbitmq
            RabbitMqEventBus rabbit = RabbitMqEventBus.Instance;
            rabbit.Start("localhost");

            rabbit.Subscribe<string>((data)=>
            {
                Rabbit_ReceiveEventMsg1(data.Data);
            }, "StOutput", true);
            rabbit.Subscribe<string>((data) =>
            {
                Rabbit_ReceiveEventMsg2(data.Data);
            }, "StInput");
            rabbit.Subscribe<string>((data) =>
            {
                Rabbit_ReceiveEventMsg3(data.Data);
            });

            while (true)
            {
                string data = Console.ReadLine();
                rabbit.Publish(data, "StInput");
            }
            #endregion
        }

        private static string Rabbit_ReceiveEventMsg1(string arg)
        {
            Console.WriteLine($"{DateTime.Now}----Receive1：{arg}");
            return string.Empty;
        }
        private static string Rabbit_ReceiveEventMsg2(string arg)
        {
            Console.WriteLine($"{DateTime.Now}----Receive2：{arg}");
            return string.Empty;
        }
        private static string Rabbit_ReceiveEventMsg3(string arg)
        {
            Console.WriteLine($"{DateTime.Now}----Receive3：{arg}");
            return string.Empty;
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