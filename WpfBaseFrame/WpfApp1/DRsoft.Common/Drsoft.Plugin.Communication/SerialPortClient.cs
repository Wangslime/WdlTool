using System;
using System.IO.Ports;
using System.Threading;

namespace Drsoft.Plugin.Communication
{
    public class SerialPortClient: CommunicationAbstract
    {
        public override event Func<byte[], byte[]>? OnReceiveEventMsg;
        public override event Action<Exception>? OnExceptionEvent;
        public override bool IsConnected => serialPort == null ? false : serialPort.IsOpen; 

        SerialPort serialPort = null;
        public override void Start(CommunicationParam param)
        {
            this.param = param;
            base.Start(param);
            if (param != null)
            {
                string portName = param.PortName;
                int baudRate = param.BaudRate;
                int dataBits = 8;
                Parity parity = Parity.None;
                StopBits stopBits = StopBits.One;

                if (param.DataBits != dataBits)
                {
                    dataBits = param.DataBits;
                }
                if (param.Parity != parity)
                {
                    parity = param.Parity;
                }
                if (param.StopBits != stopBits)
                {
                    stopBits = param.StopBits;
                }

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
                if (param.Timeout != 0)
                {
                    serialPort.ReadTimeout = param.Timeout;
                    serialPort.WriteTimeout = param.Timeout;
                }
                else
                {
                    serialPort.ReadTimeout = 1000;
                }
                if (param.IsUseReceiveEvent)
                {
                    serialPort.ReadTimeout = 0;
                    serialPort.DataReceived += SerialPortDataReceived;
                }

                try
                {
                    serialPort.Open();
                }
                catch (Exception ex)
                {
                    OnExceptionEvent?.Invoke(ex);
                }
            }
        }

        private void SerialPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort serialPort = (SerialPort)sender!;
            byte[] buffer = new byte[byteLength];
            int ret = serialPort.Read(buffer, 0, buffer.Length);
            //string receiveDara = encoding.GetString(buffer, 0, ret);

            buffer = OnReceiveEventMsg?.Invoke(buffer);
            if (buffer != null)
            {
                SendMsg(buffer);
            }
        }

        public override void CleanInBuffer()
        {
            serialPort?.DiscardInBuffer();
        }
        public override void CleanOutBuffer()
        {
            serialPort?.DiscardOutBuffer();
        }

        public override void Stop()
        {
            serialPort?.Close();
            serialPort?.Dispose();
            serialPort = null;
        }

        public override bool SendMsg(byte[] buffer)
        {
            if (IsConnected)
            {
                serialPort?.DiscardInBuffer();
                serialPort?.Write(buffer, 0, buffer.Length);
                return true;
            }
            else
            {
                return false;
            }
        }

        object lockObj = new object();

        public override byte[] GetResult(byte[] buffer)
        {
            byte[] by = base.GetResult(buffer);
            if (by != null)
            {
                try
                {
                    if (IsConnected)
                    {
                        lock (lockObj)
                        {
                            serialPort?.DiscardInBuffer();
                            serialPort?.Write(buffer, 0, buffer.Length);
                            buffer = new byte[byteLength];
                            
                            Thread.Sleep(50);

                            serialPort?.Read(buffer, 0, buffer.Length);
                            return buffer;
                        }
                    }
                }
                catch (Exception ex)
                {
                    OnExceptionEvent?.Invoke(ex);
                    return null;
                }
            }
            return null;
        }

        public override byte[] ReceiveMsg()
        {
            if (IsConnected)
            {
                lock (lockObj)
                {
                    try
                    {
                        byte[] buffer = new byte[byteLength];
                        serialPort.Read(buffer, 0, buffer.Length);
                        return buffer;
                    }
                    catch (Exception ex)
                    {
                        OnExceptionEvent?.Invoke(ex);
                        return null;
                    }
                }
            }
            return null;
        }
    }
}
