using System;
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
    public class SocketsTcpClient
    {
        private Socket socketClient = null;
        private byte[] buffer => new byte[1024];

        public event Func<string, string> ReceiveEventMsg;
        public event Action<Exception> LogError;

        CancellationTokenSource cts = new CancellationTokenSource();
        public bool Start(string ip = "127.0.0.1", int port = 10000)
        {
            //1 创建Socket对象
            socketClient?.Disconnect(true);
            socketClient?.Shutdown(SocketShutdown.Both);
            socketClient?.Close();
            socketClient?.Dispose();
            socketClient = null;
            socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
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
            Task.Run(() => { OnIsOpenReceiveMsg(); }, cts.Token);
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

        private async void OnIsOpenReceiveMsg()
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
                    int receiveLen = socketClient.Receive(buffer);
                    string receiveMsg = Encoding.UTF8.GetString(buffer, 0, receiveLen);
                    //Console.WriteLine(string.Format("收到服务器消息:" + receiveMsg));
                    receiveMsg = ReceiveEventMsg?.Invoke(receiveMsg);
                    if (!string.IsNullOrEmpty(receiveMsg)) 
                    {
                        SendMsg(receiveMsg);
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
        public bool SendMsg(string msg)
        {
            if (socketClient != null && socketClient.Connected)
            {
                return socketClient.Send(Encoding.UTF8.GetBytes(msg)) > 0;
            }
            return false;
        }
    }
}
