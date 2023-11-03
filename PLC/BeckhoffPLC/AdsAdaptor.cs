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

        private TcAdsClient client = new TcAdsClient();
        private DeviceInfo deviceInfo = new DeviceInfo();
        private StateInfo stateInfo = new StateInfo();
        private bool IsConnected = false;

        public bool Start()
        {
            string netId = "172.255.255.255.1.1";
            int port = 801;
            int timeout = 5000;
            client ??= new TcAdsClient();
            Connect(netId, port, timeout);
            if (IsConnected)
            {
                Dictionary<int,Type> plcDataTypeHandle = new Dictionary<int,Type>();
                Type type = typeof(Global_Variables);
                PropertyInfo[] properties = type.GetProperties();
                foreach (PropertyInfo propertyInfo in properties)
                {
                    try
                    {
                        int notifyHandle = CreateVariableHandle($".{propertyInfo.Name}");
                        Type type1 = propertyInfo.PropertyType;
                        if (!type.Assembly.GetTypes().Contains(type1))
                        {

                            dynamic obj1 = client.ReadAny(notifyHandle, type1);
                        }
                        else 
                        {
                        
                        }
                        plcDataTypeHandle.TryAdd(notifyHandle, propertyInfo.PropertyType);
                    }
                    catch (Exception ex)
                    {
                        
                    }
                }
                if (plcDataTypeHandle.Any())
                {
                    ReadState();
                    ReadDeviceInfo();

                    ReadAll(plcDataTypeHandle);
                    Task.Factory.StartNew(async ()=>
                    {
                        await ReadPlcInfoObj(plcDataTypeHandle);
                    }, TaskCreationOptions.LongRunning);
                }
            }
            return client.IsConnected;
        }

        private async Task ReadPlcInfoObj(Dictionary<int, Type> plcDataTypeHandle)
        {
            while (!cts.IsCancellationRequested) 
            {
                try
                {
                    ReadState();

                    await Task.Delay(50);
                }
                catch (Exception)
                {
                    await Task.Delay(1000);
                }
                
            }
        }

        public void ReadAll(Dictionary<int, Type> plcDataTypeHandle)
        {
            foreach (var item in plcDataTypeHandle)
            {
                try
                {
                    dynamic obj = client.ReadAny(item.Key, item.Value);
                    Type type = obj.GetType();
                    if (PlcDataValue.ContainsKey(item.Key))
                    {
                        PlcDataValue.TryAdd(item.Key, obj);
                    }
                    else
                    {
                        PlcDataValue[item.Key] = obj;
                    }
                }
                catch (Exception)
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
        public bool[] AlarmList = new bool[301];
    }
}