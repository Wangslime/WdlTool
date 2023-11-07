using MyAssembly;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using TwinCAT.Ads;



namespace BeckhoffPLC
{
    //AddDeviceNotification 连接一个变量到ADS客户端 
    //ReadDeviceInfo 读取ADS服务器的版本号
    //AddDeviceNotificationEx 连接一个变量到ADS客户端 
    //ReadState 读取ADS服务器的ADS状态和设备状态
    //Connect 建立一个至ADS服务的连接 
    //ReadSymbol 读取一个变量的值，并返回object类型
    //CreateSymbolInfoLoader 创建一个新的SymbolInfoLoader类
    //ReadSymbolInfo 获取一个变量的信息
    //CreateVariableHandle 生成一个ADS变量的唯一句柄 
    //ReadWrite 将数据写入ADS服务器并读取数据
    //DeleteDeviceNotification 删除设备通知 
    //Write 写入数据到ADS服务器
    //DeleteVariableHandle 释放一个ADS变量句柄 
    //WriteAny 写入数据到ADS服务器
    //Read 从ADS服务器读取数据 
    //WriteControl 改变ADS服务器的
    //ADS状态和设备状态
    //ReadAny 从ADS服务器读取数据 
    //WriteSymbol 写入一个变量的值

    public class AdsAdaptor
    {
        //存储PLC定义的结构对外的 变量句柄和对象
        private ConcurrentDictionary<int, dynamic> PlcDataValue = new ConcurrentDictionary<int, dynamic>();
        private CancellationTokenSource cts = new CancellationTokenSource();



                        {
            client ??= new AdsClient();
            client.Connect(netId, port);  // 替换 "NETID" 为实际的 Beckhoff PLC 的 NET ID
                        
            return client.IsConnected;
        }

                    {
                {
                }
            }
        }

        public void Connect(string netId, int port, int timeout)
        {
            client.Connect(netId, port);
            client.Timeout = timeout;
            IsConnected = client.IsConnected;
        }
        public void Dispose()
        {
            cts.Cancel();
            client.Dispose();
        }
        public void ReadState()
        {
            StateInfo stateInfo1 = client.ReadState();
            if (stateInfo1.AdsState != stateInfo.AdsState)
            {
                stateInfo = stateInfo1;
            }
        }
        public void ReadDeviceInfo()
        {
            deviceInfo = client.ReadDeviceInfo();
        }
        public int CreateVariableHandle(string symbolPath)
        {
            int resultHandle = client.CreateVariableHandle(symbolPath);
            return resultHandle;
        }


    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class AlarmClass
    {
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 301)]
        public bool[] Alarm = new bool[301];
    }
}