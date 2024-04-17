using FeatureCommon.Configuration;

namespace Drsoft.ConfigMamager
{
    public class AppConfig
    {
        public CommunitionConfig Communition { get; set; }
        public DbConfig DbConfig { get; set; }
    }

    public class CommunitionConfig
    {
        public List<TcpClientConfig> TCPClient { get; set; }
        public List<TcpServerConfig> TCPServer { get; set; }
        public List<ComConfig> COM { get; set; }
    }

    public class TcpClientConfig
    {
        public string Name { get; set; } = "";
        public bool Enable { get; set; }
        public bool IsUseReceiveEvent { get; set; }
        public string Ip { get; set; } = "";
        public int Port { get; set; }
        public int Timeout { get; set; }
    }

    public class TcpServerConfig
    {
        public string Name { get; set; } = "";
        public bool Enable { get; set; }
        public int Port { get; set; }
        public int Timeout { get; set; }
    }

    public class ComConfig
    {
        public string Name { get; set; } = "";
        public bool Enable { get; set; }
        public bool IsUseReceiveEvent { get; set; }
        public string PortName { get; set; } = "";
        public int BaudRate { get; set; }
        public int DataBits { get; set; }
        public string StopBits { get; set; } = "";
        public string Parity { get; set; } = "";
        public int Timeout { get; set; }
    }
}
