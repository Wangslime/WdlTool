using NModbus;
using System.Net.Sockets;

namespace WdlModbusTcp
{
    internal class Program
    {
        private static ModbusFactory modbusFactory;
        private static IModbusMaster master;
        //写线圈或写寄存器数组
        static bool[] coilsBuffer;
        static ushort[] registerBuffer;
        static //功能码
         string functionCode;
        static //参数(分别为站号,起始地址,长度)
         byte slaveAddress = 1;
        static ushort startAddress = 0;
        static ushort numberOfPoints = 5;

        static void Main(string[] args)
        {
            //初始化modbusmaster
            modbusFactory = new ModbusFactory();
            //在本地测试 所以使用回环地址,modbus协议规定端口号 502
            master = modbusFactory.CreateMaster(new TcpClient("127.0.0.1", 502));
            //设置读取超时时间
            master.Transport.ReadTimeout = 2000;
            master.Transport.Retries = 2000;

        }



        public static async void Write()
        {
            master = modbusFactory.CreateMaster(new TcpClient("127.0.0.1", 502));

            //写单个线圈
            await master.WriteSingleCoilAsync(slaveAddress, startAddress, coilsBuffer[0]);

            //写单个输入线圈/离散量线圈
            await master.WriteSingleRegisterAsync(slaveAddress, startAddress, registerBuffer[0]);

            //写一组线圈
            await master.WriteMultipleCoilsAsync(slaveAddress, startAddress, coilsBuffer);

            //写一组保持寄存器
            await master.WriteMultipleRegistersAsync(slaveAddress, startAddress, registerBuffer);

            master.Dispose();
        }


        private static void Read()
        {
            master = modbusFactory.CreateMaster(new TcpClient("127.0.0.1", 502));

            //读取单个线圈
            coilsBuffer = master.ReadCoils(slaveAddress, startAddress, numberOfPoints);


            //读取输入线圈/离散量线圈
            coilsBuffer = master.ReadInputs(slaveAddress, startAddress, numberOfPoints);


            //读取保持寄存器
            registerBuffer = master.ReadHoldingRegisters(slaveAddress, startAddress, numberOfPoints);


            //读取输入寄存器
            registerBuffer = master.ReadInputRegisters(slaveAddress, startAddress, numberOfPoints);

            master.Dispose();
        }
    }
}