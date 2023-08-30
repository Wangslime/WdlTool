using Invengo.NetAPI.Core;
using Invengo.NetAPI.Protocol.IRP1;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace RFIDWriteEpc
{
    public partial class Form1 : Form
    {
        public string rfid0X = "";

        public string ReaderName = "";

        public string ConnectType = "";

        public string ConnectAddress = "";

        public static volatile bool isPowerOff;

        public string TID = "";

        public bool isWriteEpc = false;

        public bool isRun = false;

        public Reader reader = null;

        public Form1()
        {
            this.InitializeComponent();
            this.Init();
        }

        private void Init()
        {
            this.ReaderName = ConfigurationManager.AppSettings["ReaderName"];
            this.ConnectType = ConfigurationManager.AppSettings["ConnectType"];
            this.ConnectAddress = ConfigurationManager.AppSettings["ConnectAddress"];
        }

        private void Btb_Connect_Click(object sender, EventArgs e)
        {
            bool flag = this.Btb_Connect.Text == "连接";
            if (flag)
            {
                try
                {
                    this.reader = new Reader(this.ReaderName, this.ConnectType, this.ConnectAddress);
                    bool flag2 = this.reader.Connect();
                    if (flag2)
                    {
                        this.reader.OnMessageNotificationReceived += new MessageNotificationReceivedHandle(this.reader_OnMessageNotificationReceived);
                        byte[] transmitterData = new ReadTag(ReadTag.ReadMemoryBank.TID_6C)
                        {
                            PortType = ""
                        }.TransmitterData;
                        byte[] transmitterData2 = new PowerOff
                        {
                            PortType = ""
                        }.TransmitterData;
                        byte[] array = new byte[5 + transmitterData.Length + transmitterData2.Length];
                        array[0] = 1;
                        array[1] = 1;
                        array[2] = 1;
                        array[3] = 0;
                        array[4] = 30;
                        Array.Copy(transmitterData, 0, array, 5, transmitterData.Length);
                        Array.Copy(transmitterData2, 0, array, 5 + transmitterData.Length, transmitterData2.Length);
                        SysConfig_800 msg = new SysConfig_800(226, array);
                        bool flag3 = this.reader.Send(msg);
                        Thread.Sleep(300);
                        this.WriteEpc(this.reader);
                        this.Btb_Connect.Text = "断开";
                    }
                    else
                    {
                        MessageBox.Show("连接RFIDReder失败，请检查配置文件！！！");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                try
                {
                    bool flag4 = this.reader != null;
                    if (flag4)
                    {
                        this.reader.Send(new PowerOff());
                        this.reader.Disconnect();
                        this.reader.OnMessageNotificationReceived -= new MessageNotificationReceivedHandle(this.reader_OnMessageNotificationReceived);
                    }
                    this.Btb_Connect.Text = "连接";
                }
                catch (Exception ex2)
                {
                    MessageBox.Show(ex2.Message);
                }
            }
        }

        public void reader_OnMessageNotificationReceived(BaseReader reader, IMessageNotification msg)
        {
            string protocolVersion = reader.ProtocolVersion;
            string a = protocolVersion;
            if (a == "IRP1")
            {
                string text = msg.GetMessageType();
                text = text.Substring(text.LastIndexOf('.') + 1);
                string text2 = text;
                string a2 = text2;
                if (!(a2 == "RXD_TagData"))
                {
                    if (a2 == "RXD_IOTriggerSignal_800")
                    {
                        RXD_IOTriggerSignal_800 rXD_IOTriggerSignal_ = (RXD_IOTriggerSignal_800)msg;
                        bool isStart = rXD_IOTriggerSignal_.ReceivedMessage.IsStart;
                        if (isStart)
                        {
                            Form1.isPowerOff = false;
                        }
                        else
                        {
                            Form1.isPowerOff = true;
                        }
                    }
                }
                else
                {
                    RXD_TagData rXD_TagData = (RXD_TagData)msg;
                    string text3 = Util.ConvertByteArrayToHexWordString(rXD_TagData.ReceivedMessage.TID);
                    bool flag = string.IsNullOrEmpty(this.TID);
                    if (flag)
                    {
                        this.TID = text3;
                    }
                    else
                    {
                        bool flag2 = this.TID != text3;
                        if (!flag2)
                        {
                            return;
                        }
                        this.TID = text3;
                    }
                    bool flag3 = !Form1.isPowerOff;
                    if (flag3)
                    {
                        Form1.isPowerOff = true;
                        ThreadParam threadParam = new ThreadParam();
                        threadParam.msg = msg;
                        threadParam.reader = reader;
                        Thread thread = new Thread(new ParameterizedThreadStart(this.threadMethod));
                        thread.Start(threadParam);
                    }
                }
            }
        }

        public void threadMethod(object param)
        {
            while (!this.isWriteEpc)
            {
                Thread.Sleep(300);
            }
            this.isWriteEpc = false;
            ThreadParam threadParam = param as ThreadParam;
            threadParam.reader.Send(new PowerOff());
            RXD_TagData rXD_TagData = (RXD_TagData)threadParam.msg;
            string text = Util.ConvertByteArrayToHexWordString(rXD_TagData.ReceivedMessage.TID);
            byte[] pwd = this.getPwd("00000000");
            string str = Convert.ToString(long.Parse(this.txtRfidNo.Text), 16);
            byte[] writeData = this.getWriteData(str);
            WriteEpc msg = new WriteEpc(1, pwd, writeData, rXD_TagData.ReceivedMessage.TID, MemoryBank.TIDMemory);
            bool flag = threadParam.reader.Send(msg);
            if (flag)
            {
                base.Invoke(new Action(delegate
                {
                    this.txtRfidNo.Text = (long.Parse(this.txtRfidNo.Text) + 1L).ToString();
                }));
                this.WriteEpc(threadParam.reader);
                Form1.isPowerOff = false;
            }
        }

        private byte[] getPwd(string pwdText)
        {
            bool flag = pwdText == "";
            byte[] result;
            if (flag)
            {
                result = new byte[4];
            }
            else
            {
                byte[] array = Util.ConvertHexStringToByteArray(pwdText);
                bool flag2 = array.Length < 4;
                if (flag2)
                {
                    byte[] array2 = new byte[4];
                    Array.Copy(array, 0, array2, 4 - array.Length, array.Length);
                    result = array2;
                }
                else
                {
                    result = array;
                }
            }
            return result;
        }

        private byte[] getWriteData(string str)
        {
            byte[] array = Util.ConvertHexStringToByteArray(str);
            bool flag = array.Length % 2 == 1;
            byte[] result;
            if (flag)
            {
                byte[] array2 = new byte[array.Length + 1];
                Array.Copy(array, 0, array2, 0, array.Length);
                result = array2;
            }
            else
            {
                result = array;
            }
            return result;
        }

        public void WriteEpc(BaseReader reader)
        {
            SysConfig_800 msg = new SysConfig_800(2, new byte[]
            {
                1
            });
            reader.Send(msg);
            ReadTag msg2 = new ReadTag(ReadTag.ReadMemoryBank.TID_6C);
            reader.Send(msg2);
        }

        private void BtnWriteIn_Click(object sender, EventArgs e)
        {
            bool flag = this.txtRfidNo.Text.Length != 9;
            if (flag)
            {
                MessageBox.Show("输入的RFID号必须是9位");
            }
            else
            {
                this.isWriteEpc = true;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                bool flag = this.reader != null;
                if (flag)
                {
                    this.reader.Send(new PowerOff());
                    this.reader.Disconnect();
                    this.reader.OnMessageNotificationReceived -= new MessageNotificationReceivedHandle(this.reader_OnMessageNotificationReceived);
                    this.Btb_Connect.Text = "连接";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Process.GetCurrentProcess().Kill();
        }
    }
}
