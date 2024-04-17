using Drsoft.Plugin.Communication;
using DRsoft.Runtime.Core.Nlog;
using DRSoft.Plugin.CameraVisual;

namespace Drsoft.PowerMeter.Com
{
    public class ElectricalByCom : AbstractVisual
    {
        Logger eleLog = NLogger.Instance.GetLogger("Electrical");
        public int RWStep { get; set; } = 0;
        public ElectricalByCom(CommunicationAdaptor adaptor) : base(adaptor, nameof(ElectricalByCom))
        {

        }

        public override void TriggerSend()
        {
            eleLog.Info($"{nameof(PowerMeterByCom)} send message:{RWStep}");
            SendData();
        }

        /// <summary>
        /// 发送读取数据
        /// </summary>
        private void SendData()                              //20230828
        {
            int crc = 0;
            byte[] RSendData = new byte[8];
            string SendData1 = string.Empty;
            switch (RWStep)
            {
                /////////读取温度
                case 0:

                    RSendData[0] = 0x11;
                    RSendData[1] = 0x03;
                    RSendData[2] = 0x01;
                    RSendData[3] = 0x35;
                    RSendData[4] = 0x00;
                    RSendData[5] = 0x01;
                    crc = crc16(RSendData, 6);
                    RSendData[6] = Convert.ToByte(crc & 0xFF);      //取校验码的低八位
                    RSendData[7] = Convert.ToByte(crc >> 8);        //取校验码的高八位  
                    //ComElectrical.Write(RSendData, 0, 8);
                    communication?.SendMsg(RSendData);
                    RWStep = 1;

                    break;

                case 1:

                    RSendData[0] = 0x11;
                    RSendData[1] = 0x03;
                    RSendData[2] = 0x01;
                    RSendData[3] = 0x36;
                    RSendData[4] = 0x00;
                    RSendData[5] = 0x01;
                    crc = crc16(RSendData, 6);
                    RSendData[6] = Convert.ToByte(crc & 0xFF);      //取校验码的低八位
                    RSendData[7] = Convert.ToByte(crc >> 8);        //取校验码的高八位  
                    //ComElectrical.Write(RSendData, 0, 8);
                    communication?.SendMsg(RSendData);
                    RWStep = 2;

                    break;

                case 2:

                    RSendData[0] = 0x11;
                    RSendData[1] = 0x03;
                    RSendData[2] = 0x01;
                    RSendData[3] = 0x37;
                    RSendData[4] = 0x00;
                    RSendData[5] = 0x01;
                    crc = crc16(RSendData, 6);
                    RSendData[6] = Convert.ToByte(crc & 0xFF);      //取校验码的低八位
                    RSendData[7] = Convert.ToByte(crc >> 8);        //取校验码的高八位  
                    //ComElectrical.Write(RSendData, 0, 8);
                    communication?.SendMsg(RSendData);
                    RWStep = 3;

                    break;

                case 3:

                    RSendData[0] = 0x11;
                    RSendData[1] = 0x03;
                    RSendData[2] = 0x01;
                    RSendData[3] = 0x39;
                    RSendData[4] = 0x00;
                    RSendData[5] = 0x01;
                    crc = crc16(RSendData, 6);
                    RSendData[6] = Convert.ToByte(crc & 0xFF);      //取校验码的低八位
                    RSendData[7] = Convert.ToByte(crc >> 8);        //取校验码的高八位  
                    //ComElectrical.Write(RSendData, 0, 8);
                    communication?.SendMsg(RSendData);
                    RWStep = 4;

                    break;

                case 4:

                    RSendData[0] = 0x11;
                    RSendData[1] = 0x03;
                    RSendData[2] = 0x01;
                    RSendData[3] = 0x3A;
                    RSendData[4] = 0x00;
                    RSendData[5] = 0x01;
                    crc = crc16(RSendData, 6);
                    RSendData[6] = Convert.ToByte(crc & 0xFF);      //取校验码的低八位
                    RSendData[7] = Convert.ToByte(crc >> 8);        //取校验码的高八位  
                    //ComElectrical.Write(RSendData, 0, 8);
                    communication?.SendMsg(RSendData);
                    RWStep = 5;

                    break;

                case 5:

                    RSendData[0] = 0x11;
                    RSendData[1] = 0x03;
                    RSendData[2] = 0x01;
                    RSendData[3] = 0x3B;
                    RSendData[4] = 0x00;
                    RSendData[5] = 0x01;
                    crc = crc16(RSendData, 6);
                    RSendData[6] = Convert.ToByte(crc & 0xFF);      //取校验码的低八位
                    RSendData[7] = Convert.ToByte(crc >> 8);        //取校验码的高八位  
                    //ComElectrical.Write(RSendData, 0, 8);
                    communication?.SendMsg(RSendData);
                    RWStep = 6;

                    break;

                case 6:

                    RSendData[0] = 0x11;
                    RSendData[1] = 0x03;
                    RSendData[2] = 0x01;
                    RSendData[3] = 0x46;
                    RSendData[4] = 0x00;
                    RSendData[5] = 0x01;
                    crc = crc16(RSendData, 6);
                    RSendData[6] = Convert.ToByte(crc & 0xFF);      //取校验码的低八位
                    RSendData[7] = Convert.ToByte(crc >> 8);        //取校验码的高八位  
                    //ComElectrical.Write(RSendData, 0, 8);
                    communication?.SendMsg(RSendData);
                    RWStep = 7;

                    break;

                case 7:

                    RSendData[0] = 0x11;
                    RSendData[1] = 0x03;
                    RSendData[2] = 0x01;
                    RSendData[3] = 0x47;
                    RSendData[4] = 0x00;
                    RSendData[5] = 0x01;
                    crc = crc16(RSendData, 6);
                    RSendData[6] = Convert.ToByte(crc & 0xFF);      //取校验码的低八位
                    RSendData[7] = Convert.ToByte(crc >> 8);        //取校验码的高八位  
                    //ComElectrical.Write(RSendData, 0, 8);
                    communication?.SendMsg(RSendData);
                    RWStep = 8;

                    break;

                case 8:

                    RSendData[0] = 0x11;
                    RSendData[1] = 0x03;
                    RSendData[2] = 0x01;
                    RSendData[3] = 0x48;
                    RSendData[4] = 0x00;
                    RSendData[5] = 0x01;
                    crc = crc16(RSendData, 6);
                    RSendData[6] = Convert.ToByte(crc & 0xFF);      //取校验码的低八位
                    RSendData[7] = Convert.ToByte(crc >> 8);        //取校验码的高八位  
                    //ComElectrical.Write(RSendData, 0, 8);
                    communication?.SendMsg(RSendData);
                    RWStep = 9;

                    break;

                case 9:

                    RSendData[0] = 0x11;
                    RSendData[1] = 0x03;
                    RSendData[2] = 0x01;
                    RSendData[3] = 0x49;
                    RSendData[4] = 0x00;
                    RSendData[5] = 0x01;
                    crc = crc16(RSendData, 6);
                    RSendData[6] = Convert.ToByte(crc & 0xFF);      //取校验码的低八位
                    RSendData[7] = Convert.ToByte(crc >> 8);        //取校验码的高八位  
                    //ComElectrical.Write(RSendData, 0, 8);
                    communication?.SendMsg(RSendData);
                    RWStep = 10;

                    break;

                case 10:

                    RSendData[0] = 0x11;
                    RSendData[1] = 0x03;
                    RSendData[2] = 0x01;
                    RSendData[3] = 0x5E;
                    RSendData[4] = 0x00;
                    RSendData[5] = 0x02;
                    crc = crc16(RSendData, 6);
                    RSendData[6] = Convert.ToByte(crc & 0xFF);      //取校验码的低八位
                    RSendData[7] = Convert.ToByte(crc >> 8);        //取校验码的高八位  
                    //ComElectrical.Write(RSendData, 0, 8);
                    communication?.SendMsg(RSendData);
                    RWStep = 0;

                    break;
            }

        }

        /*********************************************************************************************************
      ** 函数名称 ：crc16
      ** 函数功能 ：CRC校验
      ** 入口参数 ：
      ** 出口参数 ： 
      *********************************************************************************************************/
        private int crc16(byte[] SendData, int len)
        {
            int i = 0, j = 0, tmp = 0, CRC16;
            CRC16 = 0xFFFF;
            for (i = 0; i < len; i++)
            {
                CRC16 = SendData[i] ^ CRC16;
                for (j = 0; j < 8; j++)
                {
                    tmp = CRC16 & 0x0001;
                    CRC16 = CRC16 >> 1;
                    if (tmp != 0)
                        CRC16 = CRC16 ^ 0xa001;
                }
            }
            return (CRC16);
        }

        public override byte[] OnReceiveEventMsg(byte[] bytes)
        {
            try
            {
                string recvMessage = string.Empty;
                int DataReceive = bytes[3] * 16 * 16 + bytes[4];
                int DataReceive1 = bytes[3] * 16 * 16 * 16 * 16 * 16 * 16 + bytes[4] * 16 * 16 * 16 * 16 + bytes[5] * 16 * 16 + bytes[6];
                string DataBuffer1 = DataReceive.ToString();
                string DataBuffer2 = DataReceive1.ToString();
                recvMessage = DataBuffer1 + ":" + DataBuffer2;

                eleLog.Info($"{nameof(PowerMeterByCom)} receive message:{recvMessage}");

                if (recvMessage != null)
                {
                    recvMessage = recvMessage.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");   //去除字符串中的空格，回车，换行符，制表符
                    string[] DataBuffer = recvMessage.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);  //获取字符串数组

                    Electricaldata electricalData = new Electricaldata();

                    electricalData.data[0] = Convert.ToDouble(DataBuffer[0]);
                    electricalData.data[1] = Convert.ToDouble(DataBuffer[1]);

                    eventBus.Publish<(Electricaldata, int)>(nameof(ElectricalByCom), (electricalData, RWStep));
                }
            }
            catch (Exception ex)
            {
                catchEx.Log($"{nameof(PowerMeterByCom)} OnReceiveEventMsg ", ex);
            }
            return null;
        }
    }
}