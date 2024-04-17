using Drsoft.ConfigMamager;
using Drsoft.Plugin.CatchException;
using Drsoft.Plugin.ICommunication;
using Drsoft.PowerMeter.Com;
using DRsoft.Runtime.Core.EventBusLib;
using DRsoft.Runtime.Core.Nlog;
using DRSoft.Plugin.CameraVisual;
using FeatureCommon.Ci;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Drsoft.Bussiness.Engine
{
    /// <summary>
    /// 控制引擎基类
    /// </summary>
    public partial class EngineManager
    {
        private readonly NLogger logger = NLogger.Instance;
        private readonly EventBus eventBus = EventBus.Instance;
        private readonly CatchEx catchEx = CatchEx.Instance;
        private readonly IVisualCamera? aOICamera;
        public readonly IVisualCamera? cCDOneController;
        public readonly IVisualCamera? cCDTwoController1;
        public readonly IVisualCamera? cCDTwoController2;
        private readonly IVisualCamera? pLCamera;
        private readonly IElectricalBase? electrical;
        private readonly IPowerMeterBase? powerMeter;
        private readonly ITcpServer tcpServer;

        public readonly int MarkingCardNumber;
        private readonly bool MarkingCardSimulate;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public EngineManager(IServiceProvider sProvider, AppConfig appConfig, ITcpServer tcpServer)
        {
            this.tcpServer = tcpServer;

            //IEnumerable<IVisualCamera> visuals = sProvider.GetServices<IVisualCamera>();
            IEnumerable<IVisualCamera> visuals = Ps.sProvider.GetServices<IVisualCamera>();
            foreach (IVisualCamera item in visuals)
            {
                switch (item.cameraName)
                {
                    case nameof(AOICamera):
                        this.aOICamera = item;
                        break;
                    case nameof(CCDOneController):
                        this.cCDOneController = item;
                        break;
                    case nameof(CCDTwoController1):
                        this.cCDTwoController1 = item;
                        break;
                    case nameof(CCDTwoController2):
                        this.cCDTwoController2 = item;
                        break;
                    case nameof(PLCamera):
                        this.pLCamera = item;
                        break;
                    default:
                        break;
                }
            }
            this.electrical  = Ps.sProvider.GetRequiredService<IElectricalBase>();
            this.powerMeter = Ps.sProvider.GetRequiredService<IPowerMeterBase>();


            tcpServer.OnClientConnect += TcpServer_OnClientConnect;
            tcpServer.OnClientDisConnect += TcpServer_OnClientDisConnect;
            tcpServer.OnException += TcpServer_OnException;
            tcpServer.OnReceiveClientMsg += TcpServer_OnReceiveClientMsg;


            eventBus.Subscribe<WafersPosData>((data) =>
            {
                switch (data.EventName)
                {
                    case nameof(AOICamera):
                       
                        break;
                    case nameof(CCDOneController):
                       
                        break;
                    case nameof(CCDTwoController1):
                       
                        break;
                    case nameof(CCDTwoController2):
                        
                        break;
                    default:
                        break;
                }
            });
            eventBus.Subscribe<string[]>((data) =>
            {
                switch (data.EventName)
                {
                    case nameof(PLCamera):
                        
                        break;
                    default:
                        break;
                }
            });
            
            eventBus.Subscribe<double>((data) =>
            {
                switch (data.EventName)
                {
                    case nameof(PowerMeterByCom):
                        
                        break;
                    default:
                        break;
                }
            });
        }

        private byte[] TcpServer_OnReceiveClientMsg(string point, byte[] arg)
        {
            //收到对应的客户端发送过来的数据，可以直接处理并发送，返回值为发送给当前客户端的数据
            byte[] retByte = new byte[arg.Length];
            switch (point)
            {
                case "192.168.0.1:60000":
                    // do
                    return retByte;
                case "192.168.0.1:60001":
                    // do
                    return retByte;
                default:
                    break;
            }
            return null;
        }

        private void TcpServer_OnException(Exception obj)
        {
            throw new NotImplementedException();
        }

        private void TcpServer_OnClientDisConnect(string obj)
        {
            throw new NotImplementedException();
        }

        private void TcpServer_OnClientConnect(string obj)
        {
            throw new NotImplementedException();
        }

        private void SyncValue<T>(T oldObj, T newObj)
        {
            if (newObj == null)
            {
                oldObj = default;
                return;
            }
            PropertyInfo[] propertyInfos = typeof(T).GetProperties();
            foreach (var item in propertyInfos)
            {
                dynamic? obj1 = item.GetValue(oldObj);
                dynamic? obj2 = item.GetValue(newObj);
                if (obj2 == null)
                {
                    item.SetValue(oldObj, null);
                }
                if (item.PropertyType.IsValueType || item.PropertyType == typeof(string))
                {
                    if (obj1 != obj2)
                    {
                        item.SetValue(oldObj, obj2);
                    }
                }
                else if (item.PropertyType.IsArray)
                {
                    SyncArrayValue(obj1, obj2);
                }
                else
                {
                    SyncValue(obj1, obj2);
                }
            }
        }
        private void SyncArrayValue(dynamic oldObj, dynamic newObj)
        {
            for (int i = 0; i < oldObj.Length; i++)
            {
                if (oldObj[i] == (newObj[i]))
                {
                    oldObj[i] = newObj[i];
                }
            }
        }

        public virtual void Dispose()
        {
            tcpServer?.Dispose();
            aOICamera?.Dispose();
            cCDOneController?.Dispose();
            cCDTwoController1?.Dispose();
            cCDTwoController2?.Dispose();
            pLCamera?.Dispose();
            electrical?.Dispose();
            powerMeter?.Dispose();
        }
    }
}
