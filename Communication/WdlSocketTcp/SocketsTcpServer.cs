using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WdlSocketTcp
{
    /// <summary>
    /// 自定义Socket对象
    /// </summary>
    public class SocketsTcpServer
    {
        public event Func<string, string> ReceiveClientMsg;
        public event Action<string> ClientConnect;
        public event Action<string> ClientDisConnect;
        public event Action<Exception> LogError;
        public Encoding encoding = Encoding.UTF8;
        public int byteLength = 1024;
        public void Close()
        {
            foreach (var item in dic.Values)
            {
                try 
                {
                    item?.Shutdown(SocketShutdown.Both);
                    item?.Close();
                } catch { }
            }
            cts.Cancel();
        }
        public bool Send(string point, string data)
        {
            try
            {
                if (dic.ContainsKey(point) && dic[point].Connected)
                {
                    return dic[point].Send(encoding.GetBytes(data)) > 0;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //记录通信用的Socket
        ConcurrentDictionary<string, Socket> dic = new ConcurrentDictionary<string, Socket>();
        private CancellationTokenSource cts = new CancellationTokenSource();
        public SocketsTcpServer(int port = 13000) 
        {
            IPEndPoint point = new IPEndPoint(IPAddress.Any, 13000);//IPAddress.Any本机任何网卡IP。本机端口查看netstat -an
                                                                    //服务端Socket定义
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(point); //绑定IP
            socket.Listen(100);//启动监听。最大监听数,同一个时间点过来100个客户端，排队
            Console.WriteLine("服务器开始监听");
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
                    ClientConnect?.Invoke(point);
                    //Console.WriteLine(point + "连接客户端成功！");
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
                    LogError?.Invoke(ex);
                }
            }
            socket.Close();
        }
        //接收消息
        private void ReceiveMsg(Socket clientSocket)
        {
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
                        string point = clientSocket.RemoteEndPoint.ToString();
                        ClientDisConnect?.Invoke(point);
                        dic.TryRemove(point, out _);
                        break;
                    }
                    else 
                    {
                        //将字节转换成字符串
                        string words = encoding.GetString(buffer, 0, n);
                        //Console.WriteLine(clientSocket.RemoteEndPoint.ToString() + ":" + words);
                        //byte[] msg = encoding.GetBytes(words);
                        words = ReceiveClientMsg?.Invoke(words);
                        if (!string.IsNullOrEmpty(words))
                        {
                            clientSocket.Send(encoding.GetBytes(words));//发送数据，字节数组
                        }
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
    }
}
