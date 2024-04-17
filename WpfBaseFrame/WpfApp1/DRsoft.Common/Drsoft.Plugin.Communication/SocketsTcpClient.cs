using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Drsoft.Plugin.Communication
{
    /// <summary>
    /// 自定义Socket对象
    /// </summary>
    public class SocketsTcpClient : CommunicationAbstract
    {
        public override event Func<byte[], byte[]>? OnReceiveEventMsg;
        public override event Action<Exception>? OnExceptionEvent;
        public override bool IsConnected => socketClient == null ? false : socketClient.Connected;
        private Socket? socketClient = null;

        CancellationTokenSource cts = null;

        public override void Start(CommunicationParam param)
        {
            this.param = param;
            base.Start(param);
            if (param != null)
            {
                string ip = param.Ip;
                int port = param.Port;

                //1 创建Socket对象
                if (IsConnected)
                {
                    socketClient?.Disconnect(false);
                }
                socketClient?.Close();
                socketClient?.Dispose();
                socketClient = null;
                socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                if (param.Timeout != 0)
                {
                    socketClient.ReceiveTimeout = param.Timeout;
                    socketClient.SendTimeout = param.Timeout;
                }
                else
                {
                    socketClient.ReceiveTimeout = 1000;
                }
                //2 连接到服务端
                IPAddress iPAddress = IPAddress.Parse(ip);
                IPEndPoint ipEndPoint = new IPEndPoint(iPAddress, port);

                try
                {
                    socketClient.Connect(ipEndPoint);
                    //开线程接收服务器下来的数据
                    if (param.IsUseReceiveEvent)
                    {
                        socketClient.ReceiveTimeout = 0;
                        cts?.Cancel();
                        cts = new CancellationTokenSource();
                        TaskScheduler taskScheduler = TaskScheduler.Default;
                        Task.Factory.StartNew(() => { OnReceiveMsg(); }, cts.Token, TaskCreationOptions.LongRunning, taskScheduler);
                    }
                }
                catch (Exception ex)
                {
                    OnExceptionEvent?.Invoke(ex);
                }
            }
        }
        public override void Stop()
        {
            try
            {
                cts?.Cancel();
                if (IsConnected)
                {
                    socketClient?.Disconnect(false);
                }
                socketClient?.Close();
                socketClient?.Dispose();
            }
            catch (Exception ex)
            {
                OnExceptionEvent?.Invoke(ex);
            }
            socketClient = null;
            Thread.Sleep(10);
        }

        /// <summary>
        /// 接收服务器消息
        /// </summary>
        private void OnReceiveMsg()
        {
            while (IsConnected && !cts.Token.IsCancellationRequested)
            {
                try
                {
                    byte[] buffer = new byte[byteLength];
                    int receiveLen = socketClient.Receive(buffer);
                    //string receiveMsg = encoding.GetString(buffer, 0, receiveLen);
                    //Console.WriteLine(string.Format("收到服务器消息:" + receiveMsg));
                    if (receiveLen == 0)
                    {
                        socketClient?.Close();
                        socketClient?.Dispose();
                        // 服务端关闭连接
                        break;
                    }
                    byte[] buffer1 = null;
                    buffer1 = OnReceiveEventMsg?.Invoke(buffer);
                    if (buffer1 != null)
                    {
                        SendMsg(buffer1);
                    }
                }
                catch (Exception ex)
                {
                    OnExceptionEvent?.Invoke(ex);
                }
            }
        }

        /// <summary>
        /// 给服务器发消息
        /// </summary>
        public override bool SendMsg(byte[] buffer)
        {
            if (IsConnected)
            {
                return socketClient.Send(buffer) > 0;
            }
            return false;
        }


        object lockObj = new object();
        public override byte[] GetResult(byte[] buffer)
        {
            byte[] by = base.GetResult(buffer);
            if (by != null)
            {
                if (IsConnected)
                {
                    lock (lockObj)
                    {
                        bool ret = socketClient.Send(buffer) > 0;
                        if (ret)
                        {
                            buffer = new byte[byteLength];
                            try
                            {
                                socketClient.Receive(buffer);
                                return buffer;
                            }
                            catch (Exception ex)
                            {
                                OnExceptionEvent?.Invoke(ex);
                                return null;
                            }
                        }
                    }
                }
            }
            return null;
        }

        public override byte[] ReceiveMsg()
        {
            if (IsConnected)
            {
                lock (lockObj)
                {
                    try
                    {
                        byte[] buffer = new byte[byteLength];
                        socketClient.Receive(buffer);
                        return buffer;
                    }
                    catch (Exception ex)
                    {
                        OnExceptionEvent?.Invoke(ex);
                        return null;
                    }
                }
            }
            return null;
        }
    }
}
