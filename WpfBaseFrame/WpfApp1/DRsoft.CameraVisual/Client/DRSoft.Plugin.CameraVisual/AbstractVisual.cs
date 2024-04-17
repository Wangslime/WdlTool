using Drsoft.Plugin.CatchException;
using Drsoft.Plugin.Communication;
using Drsoft.Plugin.ICommunication;
using DRsoft.Runtime.Core.EventBusLib;
using DRsoft.Runtime.Core.Nlog;
using System.Text;

namespace DRSoft.Plugin.CameraVisual
{
    public abstract class AbstractVisual : IVisualCamera, IPowerMeterBase, IElectricalBase
    {
        public string cameraName { get; set; }
        protected NLogger logger = NLogger.Instance;
        protected EventBus eventBus = EventBus.Instance;
        protected CatchEx catchEx = CatchEx.Instance;
        public bool IsConnected => communication == null ? false : communication.IsConnected;
        public readonly CommunicationAbstract communication;

        public AbstractVisual(CommunicationAdaptor adaptor, string cameraName) 
        {
            this.cameraName = cameraName;
            communication = adaptor.GetCommunication(cameraName);
            if (communication != null)
            {
                communication.OnReceiveEventMsg += OnReceiveEventMsg;
                communication.OnExceptionEvent += OnExceptionEvent;
                Task.Run(() => { communication.Start(); });
            }
        }

        public virtual void OnExceptionEvent(Exception ex)
        {
            logger.Error(cameraName, ex);
        }

        public virtual void ReConnect()
        {
            communication?.Stop();
            communication.Reconnect();
        }

        public virtual bool SendMessageInfo(string info)
        {
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(info);
                return communication.SendMsg(bytes);
            }
            catch (Exception ex)
            {
                catchEx.Log(cameraName, ex);
                return false;
            }
        }

        public virtual byte[] OnReceiveEventMsg(byte[] bytes)
        {
            string recvMessage = Encoding.Default.GetString(bytes);
            DataReceivedEventHandler(recvMessage);
            return null;
        }

        public virtual void DataReceivedEventHandler(string recvMessage) { }

        public void Dispose()
        {
            communication?.Stop();
        }

        /// <summary>
        /// 触发Camera拍照
        /// </summary>
        public virtual void TriggerSend()
        {
            if (!IsConnected) return;
            bool ret = SendMessageInfo("A");
            if (!ret)
            {
                logger.Error($"{cameraName} send message faild, Data:A");
            }
        }
    }
}