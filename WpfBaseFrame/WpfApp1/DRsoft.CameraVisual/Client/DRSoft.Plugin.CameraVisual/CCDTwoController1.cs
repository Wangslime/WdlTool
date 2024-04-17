using Drsoft.Plugin.Communication;

namespace DRSoft.Plugin.CameraVisual
{
    public class CCDTwoController1 : AbstractVisual
    {
        public CCDTwoController1(CommunicationAdaptor adaptor) : base(adaptor, nameof(CCDTwoController1))
        {

        }

        /// <summary>
        /// 触发Camera拍照
        /// </summary>
        public void TriggerSend()
        {
            if (!IsConnected) return;
            logger.CameraVisual($"{nameof(CCDTwoController1)} send message Data:A");
            bool ret = SendMessageInfo("A");
            if (!ret)
            {
                logger.Error($"{nameof(CCDTwoController1)} send message faild, Data:A");
            }
        }


        /// <summary>
        /// WafersPosData,八位double数组：
        /// 0，DD台面1相机拍照结果，正常或NG
        /// 1，DD台面1相机X偏移
        /// 2，DD台面1相机Y偏移
        /// 3，DD台面1相机角度偏移
        /// ------------------------------
        /// 4，DD台面2相机拍照结果，正常或NG
        /// 5，DD台面2相机X偏移
        /// 6，DD台面2相机Y偏移
        /// 7，DD台面2相机角度偏移
        /// </summary>
        /// <param name="receive"></param>
        public override void DataReceivedEventHandler(string recvMessage)
        {
            try
            {
                string[] DataBuffer;
                if (recvMessage == null) return;

                logger.CameraVisual($"{nameof(CCDTwoController1)} receive message:{recvMessage}");
                recvMessage = recvMessage.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");   //去除字符串中的空格，回车，换行符，制表符
                DataBuffer = recvMessage.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);  //获取字符串数组

                if (DataBuffer.Length >= 4)
                {
                    WafersPosData wafersPosData = new WafersPosData();
                    wafersPosData.data[0] = Convert.ToDouble(DataBuffer[0]);
                    wafersPosData.data[1] = Convert.ToDouble(DataBuffer[1]);
                    wafersPosData.data[2] = Convert.ToDouble(DataBuffer[2]);
                    wafersPosData.data[3] = Convert.ToDouble(DataBuffer[3]);

                    eventBus.Publish(nameof(CCDTwoController1), wafersPosData);
                }
                else
                {
                    logger.Error($"{nameof(CCDTwoController1)} receive message:{recvMessage} length:{DataBuffer.Length}");
                }
            }
            catch (Exception ex)
            {
                catchEx.Log($"{nameof(CCDTwoController1)} DataReceivedEventHandler message:{recvMessage}", ex);
            }
        }
    }
}