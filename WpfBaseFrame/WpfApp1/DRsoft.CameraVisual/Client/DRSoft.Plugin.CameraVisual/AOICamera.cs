using Drsoft.Plugin.Communication;

namespace DRSoft.Plugin.CameraVisual
{
    public class AOICamera : AbstractVisual
    {
        public AOICamera(CommunicationAdaptor adaptor) :base(adaptor, nameof(AOICamera))
        {
            
        }

        /// <summary>
        /// 触发Camera拍照
        /// </summary>
        public override void TriggerSend()
        {
            if (!IsConnected) return;
            logger.CameraVisual($"{nameof(AOICamera)} send message Data:A");
            bool ret = SendMessageInfo("A");
            if (!ret)
            {
                logger.Error($"{nameof(AOICamera)} send message faild, Data:A");
            }
        }


        /// <summary>
        /// AOI相机收到的数据返回值
        /// </summary>
        /// <param name="receive"></param>
        public override void DataReceivedEventHandler(string recvMessage)
        {
            try
            {
                string[] DataBuffer;
                if (recvMessage == null) return;

                logger.CameraVisual($"{nameof(AOICamera)} receive message:{recvMessage}");
                recvMessage = recvMessage.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");   //去除字符串中的空格，回车，换行符，制表符
                DataBuffer = recvMessage.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);  //获取字符串数组

                if (DataBuffer.Length >= 4)
                {
                    WafersPosData wafersPosData = new WafersPosData();
                    wafersPosData.data[0] = Convert.ToDouble(DataBuffer[0]);
                    wafersPosData.data[1] = Convert.ToDouble(DataBuffer[1]);
                    wafersPosData.data[2] = Convert.ToDouble(DataBuffer[2]);
                    wafersPosData.data[3] = Convert.ToDouble(DataBuffer[3]);
                    eventBus.Publish(nameof(AOICamera), DataBuffer);
                }
                else
                {
                    logger.Error($"{nameof(AOICamera)} receive message:{recvMessage} length:{DataBuffer.Length}");
                }
            }
            catch (Exception ex)
            {
                catchEx.Log($"{nameof(AOICamera)} DataReceivedEventHandler message:{recvMessage}", ex);
            }
        }
    }
}