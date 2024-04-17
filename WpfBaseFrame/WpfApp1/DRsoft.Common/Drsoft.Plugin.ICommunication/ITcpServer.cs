using Drsoft.Plugin.ProxyAop;

namespace Drsoft.Plugin.ICommunication
{
    [ActionErrorFilter]
    [ActionExecutedFilter]
    public interface ITcpServer
    {
        event Func<string, byte[], byte[]> OnReceiveClientMsg;
        event Action<string> OnClientConnect;
        event Action<string> OnClientDisConnect;
        event Action<Exception> OnException;

        int byteLength { get; set; }
        void Start(int port);
        void Dispose();

        bool Send(string point, byte[] data);
    }
}
