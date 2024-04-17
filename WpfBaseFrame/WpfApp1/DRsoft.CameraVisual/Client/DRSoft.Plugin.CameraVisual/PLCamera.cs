using Drsoft.Plugin.Communication;

namespace DRSoft.Plugin.CameraVisual
{
    public class PLCamera : AbstractVisual
    {
        public PLCamera(CommunicationAdaptor adaptor) : base(adaptor, nameof(PLCamera))
        {

        }

        /// <summary>
        /// 触发Camera拍照
        /// </summary>
        public override void TriggerSend()
        {
            if (!IsConnected) return;
            logger.CameraVisual($"{nameof(PLCamera)} send message Data:A");
            bool ret = SendMessageInfo("A");
            if (!ret)
            {
                logger.Error($"{nameof(PLCamera)} send message faild, Data:A");
            }
        }


        /// <summary>
        /// 隐裂相机收到的数据返回值
        /// </summary>
        /// <param name="receive"></param>
        public override void DataReceivedEventHandler(string recvMessage)
        {
            try
            {
                string[] DataBuffer;
                if (recvMessage == null) return;
                logger.CameraVisual($"{nameof(PLCamera)} receive message:{recvMessage}");
                recvMessage = recvMessage.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");   //去除字符串中的空格，回车，换行符，制表符
                DataBuffer = recvMessage.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);  //获取字符串数组
                eventBus.Publish<string[]>(nameof(PLCamera), DataBuffer);
            }
            catch (Exception ex)
            {
                catchEx.Log($"{nameof(PLCamera)} DataReceivedEventHandler message:{recvMessage}", ex);
            }
            
        }
    }
}