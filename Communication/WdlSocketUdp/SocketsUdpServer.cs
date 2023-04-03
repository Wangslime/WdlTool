using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WdlSocketUdp
{
    /// <summary>
    /// 自定义Socket对象
    /// </summary>
    public class SocketsUdpServer
    {
        public event Func<string, string> ReceiveClientMsg;
        public event Action<string> ClientConnect;
        public event Action<string> ClientDisConnect;
        public event Action<Exception> LogError;

        private Socket socket;
        public void Close()
        {
            cts.Cancel();
            socket?.Disconnect(false);
            socket?.Close();
            socket?.Dispose();
            socket = null;
        }

        //记录通信用的Socket
        private CancellationTokenSource cts = new CancellationTokenSource();
        public SocketsUdpServer(int port = 13000) 
        {
            IPEndPoint point = new IPEndPoint(IPAddress.Any, 13000);//IPAddress.Any本机任何网卡IP。本机端口查看netstat -an
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Bind(point); //绑定IP
            socket.Listen(100);//启动监听。最大监听数,同一个时间点过来10个客户端，排队
            Task.Run(() => { ReciveMsg(socket); }, cts.Token);
        }

        //接收消息
        private void ReciveMsg(Socket clientSocket)
        {
            while (!cts.Token.IsCancellationRequested)
            {
                //接收客户端发送过来的数据
                try
                {
                    EndPoint point = new IPEndPoint(IPAddress.Any, 0);//用来保存发送方的ip和端口号
                    byte[] buffer = new byte[1024];
                    int length = clientSocket.ReceiveFrom(buffer, ref point);//接收数据报
                    string message = Encoding.UTF8.GetString(buffer, 0, length);
                    if (!string.IsNullOrEmpty(message))
                    {
                        clientSocket.SendTo(Encoding.UTF8.GetBytes(message), point);//发送数据，字节数组
                    }
                }
                catch (Exception ex)
                {
                    LogError?.Invoke(ex);
                }
            }
            clientSocket?.Shutdown(SocketShutdown.Both);//禁止发送和接受数据
            clientSocket?.Close();//关闭socket,释放资源
        }

        public bool sendMsg(string msg, EndPoint endPoint)
        {
            return socket.SendTo(Encoding.UTF8.GetBytes(msg), endPoint) > 0;
        }
    }
}
