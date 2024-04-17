using Drsoft.Plugin.ICommunication;
using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Drsoft.Plugin.Communication
{
    /// <summary>
    /// 自定义Socket对象
    /// </summary>
    public class SocketsTcpServer : ITcpServer
    {
        public event Func<string, byte[], byte[]> OnReceiveClientMsg;
        public event Action<string> OnClientConnect;
        public event Action<string> OnClientDisConnect;
        public event Action<Exception> OnException;
        public int byteLength { get; set; } = 1024;

        //记录通信用的Socket
        ConcurrentDictionary<string, Socket> dic = new ConcurrentDictionary<string, Socket>();
        private readonly CancellationTokenSource cts;

        public SocketsTcpServer(CancellationTokenSource cts) 
        {
            this.cts = cts;
        }

        public void Start(int port) 
        {
            IPEndPoint point = new IPEndPoint(IPAddress.Any, port);//IPAddress.Any本机任何网卡IP。本机端口查看netstat -an
                                                                   //服务端Socket定义
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(point); //绑定IP
            socket.Listen(100);//启动监听。最大监听数,同一个时间点过来100个客户端，排队
            Task.Factory.StartNew(() => { AcceptInfo(socket); }, TaskCreationOptions.LongRunning);
        }

        private void AcceptInfo(Socket socket)
        {
            while (!cts.Token.IsCancellationRequested)
            {
                //通信用socket
                try
                {
                    Socket clientSocket = socket.Accept();//如果客户端有请求，生成一个新的Socket
                    string point = clientSocket.RemoteEndPoint.ToString();
                    OnClientConnect?.Invoke(point);
                    if (dic.ContainsKey(point))
                    {
                        dic[point].Shutdown(SocketShutdown.Both);
                        dic[point].Close();
                        dic.TryRemove(point, out _);
                    }
                    dic.TryAdd(point, clientSocket);
                    //接收消息

                    Task.Factory.StartNew(() => { ReceiveMsg(clientSocket); }, TaskCreationOptions.LongRunning);
                }
                catch (Exception ex)
                {
                    OnException?.Invoke(ex);
                }
            }
            socket.Close();
        }
        //接收消息
        private void ReceiveMsg(Socket clientSocket)
        {
            string? point = clientSocket?.RemoteEndPoint?.ToString();
            while (clientSocket.Connected)
            {
                //接收客户端发送过来的数据
                try
                {
                    //定义byte数组存放从客户端接收过来的数据
                    byte[] buffer = new byte[byteLength];
                    int n = clientSocket.Receive(buffer);//将接收过来的数据放到buffer中，并返回实际接受数据的长度
                    if (n == 0)
                    {
                        OnClientDisConnect?.Invoke(point);
                        dic.TryRemove(point, out _);
                        break;
                    }
                    else
                    {
                        byte[]? sendByte = OnReceiveClientMsg?.Invoke(point, buffer);
                        if (sendByte != null)
                        {
                            clientSocket.Send(sendByte);//发送数据，字节数组
                        }
                    }
                }
                catch (Exception ex)
                {
                    OnException?.Invoke(ex);
                }
            }
            clientSocket?.Disconnect(false);
            clientSocket?.Close();//关闭socket,释放资源
            clientSocket?.Dispose();
        }

        public void Dispose()
        {
            foreach (var item in dic.Values)
            {
                try
                {
                    item?.Disconnect(false);
                    item?.Close();
                    item?.Dispose();
                }
                catch { }
            }
            cts.Cancel();
        }
        public bool Send(string point, byte[] data)
        {
            try
            {
                if (dic.ContainsKey(point) && dic[point].Connected)
                {
                    return dic[point].Send(data) > 0;
                }
                return false;
            }
            catch (Exception ex)
            {
                OnException?.Invoke(ex);
                return false;
            }
        }
    }
}
