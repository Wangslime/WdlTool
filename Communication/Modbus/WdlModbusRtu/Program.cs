using Modbus.Device;
using System.IO.Ports;
using System.Net.Sockets;
using System.Text;

namespace WdlModbusRtu
{
    internal class Program
    {
        private static IModbusMaster master;
        private static SerialPort port;


        //参数(分别为站号,起始地址,长度)
        private static byte slaveAddress = 1;
        private static ushort startAddress = 0;
        private static ushort numberOfPoints = 5;


        private static bool[] coilsBuffer;
        private static ushort[] registerBuffer;

        static void Main(string[] args)
        {

            port = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One);
            port.NewLine = "\r\n";

            master = ModbusSerialMaster.CreateRtu(port);


            //port.Encoding = Encoding.ASCII;
            //master = ModbusSerialMaster.CreateAscii(port);


            //ModbusSerialSlave modbusMaster = ModbusSerialSlave.CreateRtu(1, port);
            //modbusMaster = ModbusSerialSlave.CreateAscii(1, port);


            //初始化modbusmaster


            Console.WriteLine("Hello, World!");
        }
       
        public static async void Write()
        {
            //每次操作是要开启串口 操作完成后需要关闭串口
            //目的是为了slave更换连接是不报错
            if (port.IsOpen == false)
            {
                port.Open();
            }

            //写单个线圈
            await master.WriteSingleCoilAsync(slaveAddress, startAddress, coilsBuffer[0]);

            //写单个输入线圈/离散量线圈
            await master.WriteSingleRegisterAsync(slaveAddress, startAddress, registerBuffer[0]);

            //写一组线圈
            await master.WriteMultipleCoilsAsync(slaveAddress, startAddress, coilsBuffer);

            //写一组保持寄存器
            await master.WriteMultipleRegistersAsync(slaveAddress, startAddress, registerBuffer);
        }


        private static void Read()
        {
            //每次操作是要开启串口 操作完成后需要关闭串口
            //目的是为了slave更换连接是不报错
            if (port.IsOpen == false)
            {
                port.Open();
            }

            //读取单个线圈
            coilsBuffer = master.ReadCoils(slaveAddress, startAddress, numberOfPoints);


            //读取输入线圈/离散量线圈
            coilsBuffer = master.ReadInputs(slaveAddress, startAddress, numberOfPoints);


            //读取保持寄存器
            registerBuffer = master.ReadHoldingRegisters(slaveAddress, startAddress, numberOfPoints);


            //读取输入寄存器
            registerBuffer = master.ReadInputRegisters(slaveAddress, startAddress, numberOfPoints);
        }
    }
}