using NModbus;
using System.Net.Sockets;

namespace WdlModbusTcp
{
    internal class Program
    {
        //写线圈或写寄存器数组
        //static bool[] coilsBuffer;
        //static ushort[] registerBuffer;
        //static //功能码
        // string functionCode;
        //static //参数(分别为站号,起始地址,长度)
        // byte slaveAddress = 1;
        //static ushort startAddress = 0;
        //static ushort numberOfPoints = 5;



        private static ModbusFactory modbusFactory;
        private static IModbusMaster master;

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



        public async Task Write(byte slaveAddress, ushort startAddress, byte[] byteArray)
        {
            if (byteArray != null && byteArray.Length > 260)
            {
                throw new Exception("单次写入数据不能大于260个字节长度");
            }
            ushort valueToWrite = BitConverter.ToUInt16(byteArray, 0);
            //写单个寄存器    ushort
            await master.WriteSingleRegisterAsync(slaveAddress, startAddress, valueToWrite);

            ////写单个线圈 线圈通常用于表示开关状态  bool
            //await master.WriteSingleCoilAsync(slaveAddress, startAddress, coilsBuffer[0]);

            ////写一组线圈 线圈通常用于表示开关状态  bool[]
            //await master.WriteMultipleCoilsAsync(slaveAddress, startAddress, coilsBuffer);

            ////写一组寄存器  ushort[]
            //await master.WriteMultipleRegistersAsync(slaveAddress, startAddress, registerBuffer);

            //master.Dispose();
        }

        private async Task<byte[]> Read(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            //读取保持寄存器
            ushort[] registerBuffer = await master.ReadHoldingRegistersAsync(slaveAddress, startAddress, numberOfPoints);
            if (registerBuffer != null && registerBuffer.Length > 0)
            {
                ushort value = registerBuffer[0];
                byte[] bytes = BitConverter.GetBytes(value);
                return bytes;
            }
            else
            {
                return null;
            }

            ////读取单个线圈 单个bool
            //coilsBuffer = await master.ReadCoilsAsync(slaveAddress, startAddress, numberOfPoints)[0];


            ////读取输入线圈 bool数组
            //coilsBuffer = await master.ReadInputsAsync(slaveAddress, startAddress, numberOfPoints);


            ////读取输入寄存器 ushort数组
            //registerBuffer = await master.ReadInputRegistersAsync(slaveAddress, startAddress, numberOfPoints);
        }
    }
}