using System.IO.Ports;

namespace Drsoft.Plugin.Communication
{
    public class CommunicationParam
    {
        public string Name { get; set; } = "";
        public bool Enable { get; set; }
        public bool IsUseReceiveEvent { get; set; }
        public string PortName { get; set; } = "";
        public int BaudRate { get; set; }
        public int DataBits { get; set; }
        public StopBits StopBits { get; set; }
        public Parity Parity { get; set; }
        public int Timeout { get; set; }

        //public string Name { get; set; }
        //public bool Enable { get; set; }
        public string Ip { get; set; } = "";
        public int Port { get; set; }
        //public int Timeout { get; set; }
    }
}
