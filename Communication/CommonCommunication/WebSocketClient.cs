using System;
using System.Net.WebSockets;
using System.Text;

namespace CommonCommunication
{
    public class WebSocketClient
    {
        public event Func<string, string>? ReceiveEventMsg;
        private ClientWebSocket? webSocket = null;
        private CancellationTokenSource cts = new CancellationTokenSource();
        private ArraySegment<byte> buffer = new byte[1024 * 1024];
        public bool Start(string url, int timeout = 3000)
        {
            //开始连接---------------------------------------------
            webSocket?.Abort();
            webSocket?.Dispose();
            webSocket = new ClientWebSocket();
            webSocket.Options.KeepAliveInterval = new TimeSpan(0, 0, 30);
            //不使用代理
            webSocket.Options.Proxy = null;
            //使用默认凭据
            webSocket.Options.UseDefaultCredentials = true;
            //异步连接，不设置取消令牌
            Uri uri = new Uri(url);
            IAsyncResult asyncResult = webSocket.ConnectAsync(uri, CancellationToken.None);

            bool isSuccess = asyncResult.AsyncWaitHandle.WaitOne(timeout, true);
            if (isSuccess && webSocket.State == WebSocketState.Open)
            {
                Task.Run(() => { OnIsOpenReceiveMsg(); }, cts.Token);
                return true;
            }
            return false;
        }

        public void Close() 
        {
            //断开连接---------------------------------------------
            try
            {
                if (webSocket != null && webSocket.State == WebSocketState.Open)
                {
                    IAsyncResult resultAsync = webSocket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                }
                webSocket?.Abort();
                webSocket?.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            finally 
            {
                cts.Cancel();
            }
            
        }

        public async Task SendAsync(string data)
        {
            //发送---------------------------------------------
            byte[] buffer = Encoding.UTF8.GetBytes(data);
            if (webSocket != null && webSocket.State != WebSocketState.Closed)
            {
                try
                {
                    await webSocket.SendAsync(
                     buffer
                     , WebSocketMessageType.Binary
                     , true
                     , CancellationToken.None)
                     .ConfigureAwait(false);//不需要回到await前的线程

                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                if (webSocket == null)
                {
                    throw new Exception("webSocket对象为空");
                }
                else
                {
                    throw new Exception(webSocket.State.ToString());
                }
            }
        }
        public void Send(string data)
        {
            //发送---------------------------------------------
            byte[] buffer = Encoding.UTF8.GetBytes(data);
            if (webSocket != null && webSocket.State != WebSocketState.Closed)
            {
                try
                {
                    webSocket.SendAsync(
                    buffer
                    , WebSocketMessageType.Binary
                    , true
                    , CancellationToken.None).Wait();

                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                if (webSocket == null)
                {
                    throw new Exception("webSocket对象为空");
                }
                else
                {
                    throw new Exception(webSocket.State.ToString());
                }
            }
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
                    await Task.Delay(1000, cts.Token);
                }
            }
        }

        private async void OnReceiveMsg()
        {
            while (!cts.Token.IsCancellationRequested)
            {
                //接收---------------------------------------------
                if (webSocket == null)
                {
                    break;
                }
                if (webSocket.State != WebSocketState.Closed)
                {
                    WebSocketReceiveResult receiveResult = await webSocket.ReceiveAsync(buffer, CancellationToken.None).ConfigureAwait(false);

                    if (webSocket.State == WebSocketState.Open && receiveResult.MessageType != WebSocketMessageType.Close)
                    {
                        int length = receiveResult.Count;
                        if (length > 0)
                        {
                            string msg = Encoding.UTF8.GetString(buffer.Array, buffer.Offset, length);
                            msg = ReceiveEventMsg?.Invoke(msg);
                            if (!string.IsNullOrEmpty(msg))
                            {
                                Send(msg);
                            }
                        }
                    }
                }
            }
        }
    }
}
