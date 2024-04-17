using Drsoft.ConfigMamager;
using System;
using System.Collections.Generic;
using System.IO.Ports;

namespace Drsoft.Plugin.Communication
{
    /// <summary>
    /// 对外开放对象
    /// </summary>
    public class CommunicationAdaptor
    {
        public CommunicationAdaptor(AppConfig appConfig) 
        {
            InitiaCommunition(appConfig.Communition);
        }

        public CommunicationAbstract GetCommunication(string name) => communicationAbstracts.ContainsKey(name) ? communicationAbstracts[name] : null;

        private Dictionary<string, CommunicationAbstract> communicationAbstracts = new Dictionary<string, CommunicationAbstract>();
        private void InitiaCommunition(CommunitionConfig Communition)
        {
            foreach (var tcp in Communition.TCPClient)
            {
                if (tcp.Enable)
                {
                    CommunicationAbstract communicationAbstract = new SocketsTcpClient();
                    CommunicationParam communicationParam = new CommunicationParam()
                    {
                        Name = tcp.Name,
                        Enable = tcp.Enable,
                        Ip = tcp.Ip,
                        Port = tcp.Port,
                        Timeout = tcp.Timeout,
                        IsUseReceiveEvent = tcp.IsUseReceiveEvent,
                    };
                    communicationAbstract.param = communicationParam;
                    communicationAbstracts.Add(tcp.Name, communicationAbstract);
                }
            }
            foreach (var com in Communition.COM)
            {
                if (com.Enable)
                {
                    CommunicationAbstract communicationAbstract = new SerialPortClient();
                    CommunicationParam communicationParam = new CommunicationParam()
                    {
                        Name = com.Name,
                        Enable = com.Enable,
                        PortName = com.PortName,
                        BaudRate = com.BaudRate,
                        DataBits = com.DataBits,
                        StopBits = (StopBits)Enum.Parse(typeof(StopBits), com.StopBits),
                        Parity = (Parity)Enum.Parse(typeof(Parity), com.Parity),
                        Timeout = com.Timeout,
                        IsUseReceiveEvent = com.IsUseReceiveEvent,
                    };
                    communicationAbstract.param = communicationParam;
                    //communicationAbstract.Start(communicationParam);
                    communicationAbstracts.Add(com.Name, communicationAbstract);
                }
            }
        }
    }
}
