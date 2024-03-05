using System.IO.Ports;
using System.Text;

namespace WdlSerialPort
{
    public class SerialPortClient
    {
        public int byteLength = 1024;
        public bool IsConnect => serialPort == null ? false : serialPort.IsOpen;
        SerialPort serialPort = null;
        public event Func<string, string> ReceiveMsgEvent;
        public Encoding encoding = Encoding.Default;
        public bool Start(string portName, int baudRate, int dataBits = 8, Parity parity = Parity.None, StopBits stopBits = StopBits.One)
        {
            // 设置串口属性
            if (serialPort != null)
            {
                serialPort.Close();
                serialPort.Dispose();
                serialPort = null;
            }
            serialPort ??= new SerialPort();
            serialPort.PortName = portName;
            serialPort.BaudRate = baudRate;
            serialPort.DataBits = dataBits;
            serialPort.Parity = parity;
            serialPort.StopBits = stopBits;
            serialPort.Open();
            serialPort.DataReceived += SerialPortDataReceived;
            return serialPort.IsOpen;
        }

        private void SerialPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort serialPort = (SerialPort)sender!;

            byte[] buffer = new byte[byteLength];
            int ret  = serialPort.Read(buffer, 0, buffer.Length);
            string receiveDara = encoding.GetString(buffer, 0, ret);
            string str = ReceiveMsgEvent?.Invoke(receiveDara);
            if (!string.IsNullOrEmpty(str))
            {
                SendMsg(str);
            }
        }

        public bool SendMsg(string msg)
        {
            byte[] buffer = encoding.GetBytes(msg);
            serialPort?.Write(buffer, 0, buffer.Length);
            return true;
        }
    }
}