using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Diagnostics;

namespace CommonCommunication
{
    public class AsyncTcpClient
    {
        #region Properties

        private TcpClient tcpClient;
        private int reconnectTimeOut = 10000;
        private int readWriteTimeOut = 1000;
        private string serverName = "";
        private string ip = "127.0.0.1";
        private int port = 9502;

        /// <summary>
        /// 连接状态
        /// </summary>
        private bool Connected { get { return tcpClient.Connected; } }
        /// <summary>
        /// 重连时间间隔，单位:ms
        /// </summary>
        public int ReconnectTimeOut { set { reconnectTimeOut = value; } }
        /// <summary>
        /// 读写超时时间，单位:ms
        /// </summary>
        public int ReadWriteTimeOut { set { readWriteTimeOut = value; } }
        /// <summary>
        /// 本地客户端终结点
        /// </summary>
        protected IPEndPoint LocalIPEndPoint { get; private set; }
        /// <summary>
        /// 通信所使用的编码
        /// </summary>
        public Encoding Encoding { get; set; }
        /// <summary>
        /// 线程终止
        /// </summary>
        public CancellationTokenSource cts = new CancellationTokenSource();
        #endregion

        #region Ctors
        /// <summary>
        /// 异步TCP客户端
        /// </summary>
        /// <param name="remoteIPAddresses">远端服务器IP地址列表</param>
        /// <param name="remotePort">远端服务器端口</param>
        /// <param name="localEP">本地客户端终结点</param>
        public AsyncTcpClient(string remoteIp, string remotePort)
        {
            this.ip = remoteIp;
            if (int.TryParse(remotePort, out int nPort))
            {
                this.port = nPort;
            }
            this.Encoding = Encoding.Default;
            this.tcpClient = new TcpClient();
        }

        public AsyncTcpClient()
        {
           
        }

        #endregion

        #region Connect
        public void Start()
        {
            Task.Factory.StartNew(() => { CheckConnectStatus(); }, TaskCreationOptions.LongRunning);
        }

        /// <summary>
        /// 关闭与服务器的连接
        /// </summary>
        /// <returns>异步TCP客户端</returns>
        public void Close()
        {
            if (Connected)
            {
                tcpClient.Close();
            }
            cts.Cancel();
        }

        /// <summary>
        /// 检测连接状态
        /// </summary>
        private async void CheckConnectStatus()
        {
            while (!cts.IsCancellationRequested)
            {
                try
                {
                    if (!CheckConnected())
                    {
                        tcpClient = TryConnect(ip, port, reconnectTimeOut);
                        if (tcpClient != null)
                        {
                            //设置读写超时时间
                            tcpClient.ReceiveTimeout = readWriteTimeOut;
                            tcpClient.SendTimeout = readWriteTimeOut;
                            RaiseServerConnected(true);
                            byte[] buffer = new byte[10240];
                            tcpClient.GetStream().BeginRead(buffer, 0, buffer.Length, HandleDataReceived, buffer);
                        }
                    }
                }
                catch (Exception ex)
                {
                    RaiseServerConnected(false);
                    logError?.Invoke($"CheckConnectStatus检测在线状态报错:{ex.Message}");
                }
                finally
                {
                    await Task.Delay(reconnectTimeOut);
                }
            }
        }

        private bool IsConnectionSuccessful = false;
        private Exception socketexception;
        private ManualResetEvent TimeoutObject = new ManualResetEvent(false);
        public TcpClient TryConnect(string Ip, int port, int timeoutMiliSecond)
        {
            TimeoutObject.Reset();
            socketexception = null;
            string serverip = Ip;
            int serverport = port;
            TcpClient tcpclient = new TcpClient();
            tcpclient.BeginConnect(serverip, serverport, new AsyncCallback(CallBackMethod), tcpclient);
            if (TimeoutObject.WaitOne(timeoutMiliSecond, false))
            {
                if (IsConnectionSuccessful)
                {
                    return tcpclient;
                }
                else
                {
                    throw socketexception;
                }
            }
            else
            {
                tcpclient.Close();
                throw new TimeoutException("TimeOut Exception");
            }
        }

        private void CallBackMethod(IAsyncResult asyncresult)
        {
            try
            {
                IsConnectionSuccessful = false;
                TcpClient tcpclient = asyncresult.AsyncState as TcpClient;
                if (tcpclient.Client != null)
                {
                    tcpclient.EndConnect(asyncresult);
                    IsConnectionSuccessful = true;
                }
            }
            catch (Exception ex)
            {
                IsConnectionSuccessful = false;
                socketexception = ex;
            }
            finally
            {
                TimeoutObject.Set();
            }
        }

        /// <summary>
        /// Checks the connection state
        /// </summary>
        private bool CheckConnected()
        {
            var flag = false;
            if (tcpClient != null && tcpClient.Connected)
            {
                flag = !((tcpClient.Client.Poll(1000, SelectMode.SelectRead) && (tcpClient.Client.Available == 0)) || !tcpClient.Client.Connected);
            }
            return flag;
        }
        #endregion

        #region Receive

        private void HandleDataReceived(IAsyncResult ar)
        {
            if (tcpClient != null && Connected)
            {
                NetworkStream stream = tcpClient?.GetStream();
                int numberOfReadBytes = 0;
                try
                {
                    numberOfReadBytes = stream.EndRead(ar);
                }
                catch (Exception ex)
                {
                    numberOfReadBytes = 0;
                    logError?.Invoke($"HandleDataReceived接收数据报错:{ex.Message},{ex.StackTrace}");
                }
                if (numberOfReadBytes == 0)
                {
                    // connection has been closed
                    //Close();
                    return;
                }

                // received byte and trigger event notification
                byte[] buffer = (byte[])ar.AsyncState;
                byte[] receivedBytes = new byte[numberOfReadBytes];
                Buffer.BlockCopy(buffer, 0, receivedBytes, 0, numberOfReadBytes);
                RaiseDataReceived(receivedBytes);

                // then start reading from the network again
                stream?.BeginRead(buffer, 0, buffer.Length, HandleDataReceived, buffer);
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// 接收事件
        /// </summary>
        public event Action<byte[]> EventDataReceived;
        /// <summary>
        /// 重连事件
        /// </summary>
        public event Action<string, bool> EventServerConnected;
        /// <summary>
        /// 记录错误日志
        /// </summary>
        public event Action<string> logError;

        private void RaiseDataReceived(byte[] data)
        {
            EventDataReceived?.Invoke(data);
        }

        private void RaiseServerConnected(bool isConnected)
        {
            EventServerConnected?.Invoke($"{serverName}({ip})", isConnected);
        }

        #endregion

        #region Send

        /// <summary>
        /// 发送报文
        /// </summary>
        /// <param name="data">报文</param>
        public Task Send(string tip, byte[] data)
        {
            if (data == null || data.Length == 0)
            {
                throw new ArgumentNullException("datagram");
            }
            try
            {
                return tcpClient.GetStream().WriteAsync(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                logError($"Send报错：{ex.Message}。待发送报文：{tip}");
            }
            return Task.CompletedTask;
        }
        public Task Send(string tip)
        {
            byte[] data = UTF8Encoding.UTF8.GetBytes(tip);
            if (data == null || data.Length == 0)
            {
                throw new ArgumentNullException("datagram");
            }
            try
            {
                return tcpClient.GetStream().WriteAsync(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                logError($"Send报错：{ex.Message}。待发送报文：{tip}");
            }
            return Task.CompletedTask;
        }

        public Task SendShort(string ip, int port, string tip)
        {
            
            byte[] data = UTF8Encoding.UTF8.GetBytes(tip);
            if (data == null || data.Length == 0)
            {
                throw new ArgumentNullException("datagram");
            }
            try
            {
                return tcpClient.GetStream().WriteAsync(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                logError($"Send报错：{ex.Message}。待发送报文：{tip}");
            }
            return Task.CompletedTask;
        }
        #endregion

        #region 短链接
        /// <summary>
        /// 读写锁：当前场桥
        /// </summary>
        private ReaderWriterLockSlim rwlReceiveByte = new ReaderWriterLockSlim();
        /// <summary>
        /// 当前场桥动态数据
        /// </summary>
        private byte[]? m_receiveByte = null;
        public byte[]? receiveByte
        {
            get
            {
                rwlReceiveByte.EnterReadLock();
                try
                {
                    return m_receiveByte;
                }
                finally
                {
                    rwlReceiveByte.ExitReadLock();
                }
            }
            set
            {
                rwlReceiveByte.EnterWriteLock();
                try
                {
                    m_receiveByte = value;
                }
                finally
                {
                    rwlReceiveByte.ExitWriteLock();
                }
            }
        }


        private static readonly object locked = new object();
        private static AsyncTcpClient _Instance = null;
        private static AsyncTcpClient Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (locked)
                    {
                        if (Instance == null)
                        {
                            _Instance = new AsyncTcpClient();
                        }
                    }
                }
                return _Instance;
            }
            set
            {
                lock (locked)
                {
                    _Instance = value;
                }
            }
        }
        public static Task<string> ShortStartAsync(string ip, int port, string tip, int timeout = 3000)
        {
            return Instance.PrivateShortStartAsync(ip, port, tip, timeout);
        }
        private TcpClient shortTcpClient;
        private async Task<string> PrivateShortStartAsync(string ip, int port, string tip, int timeout)
        {
            string result = "";
            shortTcpClient = new TcpClient();
            try
            {
                shortTcpClient = TryConnect(ip, port, timeout);
                //设置读写超时时间
                shortTcpClient.ReceiveTimeout = timeout;
                shortTcpClient.SendTimeout = timeout;
                byte[] buffer = new byte[10240];
                shortTcpClient.GetStream().BeginRead(buffer, 0, buffer.Length, ShortHandleDataReceived, buffer);
                byte[] data = UTF8Encoding.UTF8.GetBytes(tip);
                await shortTcpClient.GetStream().WriteAsync(data, 0, data.Length);
                result = await Task.Run(async () =>
                {
                    Stopwatch stopwatch = Stopwatch.StartNew();
                    string strPlcRead = "";
                    while (true)
                    {
                        if (stopwatch.ElapsedMilliseconds > timeout)
                        {
                            break;
                        }
                        if (receiveByte != null)
                        {
                            break;
                        }
                        await Task.Delay(30);
                    }

                    if (receiveByte != null)
                    {
                        strPlcRead = UTF8Encoding.UTF8.GetString(receiveByte);
                        receiveByte = null;
                    }
                    return strPlcRead;
                });
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                shortTcpClient?.Close();
            }
            return result;
        }

        private void ShortHandleDataReceived(IAsyncResult ar)
        {
            if (shortTcpClient != null && shortTcpClient.Connected)
            {
                NetworkStream stream = shortTcpClient?.GetStream();
                int numberOfReadBytes = 0;
                try
                {
                    numberOfReadBytes = stream.EndRead(ar);
                }
                catch (Exception ex)
                {
                    numberOfReadBytes = 0;
                    logError?.Invoke($"HandleDataReceived接收数据报错:{ex.Message},{ex.StackTrace}");
                }
                if (numberOfReadBytes == 0)
                {
                    // connection has been closed
                    //Close();
                    return;
                }

                // received byte and trigger event notification
                byte[] buffer = (byte[])ar.AsyncState;
                byte[] receivedBytes = new byte[numberOfReadBytes];
                Buffer.BlockCopy(buffer, 0, receivedBytes, 0, numberOfReadBytes);
                receiveByte = receivedBytes;
                // then start reading from the network again
                stream?.BeginRead(buffer, 0, buffer.Length, ShortHandleDataReceived, buffer);
            }
        }

        #endregion
    }
}