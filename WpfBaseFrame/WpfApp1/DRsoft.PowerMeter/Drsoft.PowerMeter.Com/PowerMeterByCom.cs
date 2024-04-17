using Drsoft.Plugin.Communication;
using DRsoft.Runtime.Core.Nlog;
using DRSoft.Plugin.CameraVisual;

namespace Drsoft.PowerMeter.Com
{
    public class PowerMeterByCom : AbstractVisual
    {
        Logger powerLog = NLogger.Instance.GetLogger("PowerMeter");
        public int RWStep { get; set; } = 0;
        public PowerMeterByCom(CommunicationAdaptor adaptor) : base(adaptor, nameof(PowerMeterByCom))
        {

        }

        public override void TriggerSend()
        {
            powerLog.Info($"{nameof(PowerMeterByCom)} send message Data:A");
            SendMessageInfo("*OUTPM :");
        }

        public override void DataReceivedEventHandler(string recvMessage)
        {
            try
            {
                powerLog.Info($"{nameof(PowerMeterByCom)} receive message:{recvMessage}");
                string[] DataBuffer;                    //20230828
                if (recvMessage == null) return;
                recvMessage = recvMessage.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");   //去除字符串中的空格，回车，换行符，制表符
                DataBuffer = recvMessage.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);  //获取字符串数组
                double data;
                double.TryParse(DataBuffer[0], out data);

                eventBus.Publish<double>(nameof(PowerMeterByCom), data);
            }
            catch (Exception ex)
            {
                catchEx.Log($"{nameof(PowerMeterByCom)} OnReceiveEventMsg ", ex);
            }
        }
    }
}