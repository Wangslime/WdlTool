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
    public class SocketsUdpClient
    {
        private Socket socketClient = null;
        public event Func<string, string> ReceiveEventMsg;
        public event Action<Exception> LogError;

        CancellationTokenSource cts = new CancellationTokenSource();
        public bool Start(string ip = "127.0.0.1", int port = 13000)
        {
            //1 创建Socket对象
            socketClient?.Disconnect(true);
            socketClient?.Shutdown(SocketShutdown.Both);
            socketClient?.Close();
            socketClient?.Dispose();
            socketClient = null;
            socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //2 连接到服务端
            IPAddress iPAddress = IPAddress.Parse(ip);
            IPEndPoint ipEndPoint = new IPEndPoint(iPAddress, port);
            try
            {
                socketClient.Connect(ipEndPoint);
            }
            catch (Exception)
            {
                throw;
            }
            //开线程接收服务器下来的数据
            Task.Run(async () => { await OnIsOpenReceiveMsg(); }, cts.Token);
            return true;
        }
        public void Stop() 
        {
            cts.Cancel();
            socketClient?.Disconnect(true);
            socketClient?.Shutdown(SocketShutdown.Both);
            socketClient?.Close();
            socketClient?.Dispose();
            socketClient = null;
        }

        private async Task OnIsOpenReceiveMsg()
        {
            while (!cts.Token.IsCancellationRequested)
            {
                try
                {
                    if (ReceiveEventMsg != null)
                    {
                        _ = Task.Run(() => { OnReceiveMsg(); });
                        break;
                    }
                }
                catch (Exception)
                {
                    _ = Task.Run(() => { OnReceiveMsg(); });
                    break;
                }
                finally
                { 
                    await Task.Delay(200, cts.Token);
                }
            }
        }
        /// <summary>
        /// 接收服务器消息
        /// </summary>
        private void OnReceiveMsg()
        {
            while (!cts.Token.IsCancellationRequested)
            {
                try
                {
                    EndPoint point = new IPEndPoint(IPAddress.Any, 0);//用来保存发送方的ip和端口号
                    byte[] buffer = new byte[1024];
                    int length = socketClient.ReceiveFrom(buffer, ref point);//接收数据报
                    string message = Encoding.UTF8.GetString(buffer, 0, length);
                    message = ReceiveEventMsg?.Invoke(message);
                    if (!string.IsNullOrEmpty(message)) 
                    {
                        SendMsg(point, message);
                    }
                }
                catch (Exception ex)
                {
                    LogError.Invoke(ex);
                    socketClient?.Disconnect(true);
                    socketClient?.Shutdown(SocketShutdown.Both);
                    socketClient?.Close();
                    socketClient?.Dispose();
                    socketClient = null;
                }
            }
        }

        /// <summary>
        /// 给服务器发消息
        /// </summary>
        public bool SendMsg(EndPoint point, string msg)
        {
            if (socketClient != null && socketClient.Connected)
            {
                return socketClient.SendTo(Encoding.UTF8.GetBytes(msg), point) > 0;
            }
            return false;
        }
    }
}
