using Invengo.NetAPI.Core;
using Invengo.NetAPI.Protocol.IRP1;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace RFIDReader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Init();
        }

        public string ConnectAddress = "";
        public string ConnectType = "";
        public Reader reader = null;
        public string ReaderName = "";
        public string rfid0X = "";


        private void Init()
        {
            this.ReaderName = ConfigurationManager.AppSettings["ReaderName"];
            this.ConnectType = ConfigurationManager.AppSettings["ConnectType"];
            this.ConnectAddress = ConfigurationManager.AppSettings["ConnectAddress"];
        }

        private void Btb_Connect_Click(object sender, EventArgs e)
        {
            if (this.Btb_Connect.Text == "连接")
            {
                try
                {
                    this.reader = new Reader(this.ReaderName, this.ConnectType, this.ConnectAddress);
                    if (this.reader.Connect())
                    {
                        this.reader.OnMessageNotificationReceived += new MessageNotificationReceivedHandle(this.reader_OnMessageNotificationReceived);
                    }
                    else
                    {
                        MessageBox.Show("连接RFIDReder失败，请检查配置文件！！！");
                        return;
                    }
                    byte[] pData = new byte[] { 1 };
                    SysConfig_800 msg = new SysConfig_800(2, pData);
                    this.reader.Send(msg);
                    ReadTag tag = new ReadTag(ReadTag.ReadMemoryBank.EPC_6C);
                    this.reader.Send(tag);
                    Thread.Sleep(300);
                    Thread.Sleep(300);
                    this.Btb_Connect.Text = "断开";
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
            else
            {
                try
                {
                    if (this.reader != null)
                    {
                        this.reader.Send(new PowerOff());
                        this.reader.Disconnect();
                        this.reader.OnMessageNotificationReceived -= new MessageNotificationReceivedHandle(this.reader_OnMessageNotificationReceived);
                    }
                    this.Btb_Connect.Text = "连接";
                }
                catch (Exception exception2)
                {
                    MessageBox.Show(exception2.Message);
                }
            }
        }



        public void reader_OnMessageNotificationReceived(BaseReader reader, IMessageNotification msg)
        {
            if (reader.ProtocolVersion == "IRP1")
            {
                if (msg.StatusCode != 0)
                {
                    Console.WriteLine(reader.ReaderName + ":" + msg.ErrInfo);
                }
                else
                {
                    string messageType = msg.GetMessageType();
                    if (messageType.Substring(messageType.LastIndexOf('.') + 1) == "RXD_TagData")
                    {
                        RXD_TagData data = (RXD_TagData)msg;
                        string str6 = Util.ConvertByteArrayToHexWordString(data.ReceivedMessage.EPC);
                        if (this.rfid0X != str6)
                        {
                            this.rfid0X = str6;
                            string str7 = str6.Trim().Replace(" ", "");
                            long rfid = Convert.ToInt64(str7, 0x10);
                            this.Invoke(new Action(() =>
                            {
                                this.txtRfidNo.Text = rfid.ToString();
                                byte[] pData = new byte[] { 1 };
                                SysConfig_800 g_ = new SysConfig_800(2, pData);
                                reader.Send(g_);
                                Clipboard.SetText(rfid.ToString());
                            }));
                            this.Invoke(new Action(() => SendKeys.Send("^{v}")));
                        }
                    }
                }
            }
        }



        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            bool flag = this.reader != null;
            if (flag)
            {
                try
                {
                    this.reader.Send(new PowerOff());
                    this.reader.Disconnect();
                    this.reader.OnMessageNotificationReceived -= new MessageNotificationReceivedHandle(this.reader_OnMessageNotificationReceived);
                    this.Btb_Connect.Text = "连接";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            Process.GetCurrentProcess().Kill();
        }
    }
}
