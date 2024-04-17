using Drsoft.Bussiness.Engine;
using Drsoft.ConfigMamager;
using Drsoft.Plugin.CatchException;
using Drsoft.Plugin.Communication;
using Drsoft.Plugin.ICommunication;
using Drsoft.Plugin.ProxyAop;
using Drsoft.Plugin.PublicExtend;
using Drsoft.PowerMeter.Com;
using DRsoft.Runtime.Core.DBService;
using DRsoft.Runtime.Core.DBService.Interface;
using DRsoft.Runtime.Core.Nlog;
using DRSoft.Plugin.CameraVisual;
using FeatureCommon;
using FeatureCommon.Ci;
using FeatureCommon.Tprc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;

namespace WpfApp1
{
    internal class StartUp
    {
        NLogger logger = NLogger.Instance;
        Logger powerLog = NLogger.Instance.GetLogger("PowerMeter");
        Logger eleLog = NLogger.Instance.GetLogger("Electrical");

        CatchEx catchEx = CatchEx.Instance;

        internal void IocConfigure()
        {
            #region 容器注入
            //1.ProxyFactory.Creat为使用AOP容器，接口必须标注特性才有效果
            //2.注入IOC容器对象的三种声明周期 1.AddSingleton  2.AddScoped  3.AddTransient
            //3.从IOC容器中取对象的三种方式  1.通过构造函数注入直接使用  2.通过静态的Ps.sProvider对象获取。 3.通过构造函数中的IServiceProvider对象获取
            Guid guid = new Guid("5f7eb967-6968-4643-9704-a48421a08dfe");
            Ps.SetProjectInfo(ProjectEnum.Vision, guid);
            Ps.BuildServiceProvider<StartUp>((services) =>
            {
                //全局控制所有线程的关闭
                services.AddSingleton<CancellationTokenSource>();

                // 配置文件注入
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "appsetting.json");
                AppConfig appConfig = filePath.ReadJsonFile<AppConfig>()!;
                services.AddSingleton(appConfig);

                //TCP和COM通讯模块注入，基础模块，配合具体业务模块使用
                services.AddSingleton<CommunicationAdaptor>();

                //视觉模块注入 使用AOP
                services.AddSingleton((sp) =>
                {
                    CommunicationAdaptor communi = sp.GetRequiredService<CommunicationAdaptor>();
                    IVisualCamera communication = new AOICamera(communi);
                    communication = ProxyFactory.Creat(communication);
                    return communication;
                });
                //视觉模块注入 普通使用接口
                //services.AddSingleton<IVisualCamera, AOICamera>();
                //视觉模块注入 普通使用抽象函数
                //services.AddSingleton<AbstractVisual, AOICamera>();
                //视觉模块注入 普通使用对象
                //services.AddSingleton<AOICamera>();

                services.AddSingleton((sp) =>
                {
                    CommunicationAdaptor communi = sp.GetRequiredService<CommunicationAdaptor>();
                    IVisualCamera communication = new CCDOneController(communi);
                    communication = ProxyFactory.Creat(communication);
                    return communication;
                });

                services.AddSingleton((sp) =>
                {
                    CommunicationAdaptor communi = sp.GetRequiredService<CommunicationAdaptor>();
                    IVisualCamera communication = new CCDTwoController1(communi);
                    communication = ProxyFactory.Creat(communication);
                    return communication;
                });
                services.AddSingleton((sp) =>
                {
                    CommunicationAdaptor communi = sp.GetRequiredService<CommunicationAdaptor>();
                    IVisualCamera communication = new CCDTwoController2(communi);
                    communication = ProxyFactory.Creat(communication);
                    return communication;
                });
                services.AddSingleton((sp) =>
                {
                    CommunicationAdaptor communi = sp.GetRequiredService<CommunicationAdaptor>();
                    IVisualCamera communication = new PLCamera(communi);
                    communication = ProxyFactory.Creat(communication);
                    return communication;
                });

                //电源器
                services.AddSingleton((sp) =>
                {
                    CommunicationAdaptor communi = sp.GetRequiredService<CommunicationAdaptor>();
                    IElectricalBase communication = new ElectricalByCom(communi);
                    communication = ProxyFactory.Creat(communication);
                    return communication;
                });

                //TCP服务端
                services.AddSingleton<ITcpServer, SocketsTcpServer>();

                //业务逻辑处理模块注入
                services.AddSingleton<EngineManager>();

                //数据库模块注入
                services.AddSingleton<IDataBase>((sp) =>
                {
                    AppConfig appConfig = sp.GetRequiredService<AppConfig>();
                    IDataBase dBService = new DBService(appConfig.DbConfig);
                    return dBService;
                });
            });
            #endregion


        }

        #region 启动全局异常捕获
        public void ApplicationException()
        {
            //全局错误处理
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;             //Thead，处理在非UI线程上未处理的异常,当前域未处理异常
            Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;   //处理在UI线程上未处理的异常
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;             //处理在Task上未处理的异常
        }

        // 全局未处理异常处理程序（非UI线程）
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception? ex = e.ExceptionObject as Exception;
            if (ex != null)
            {
                // 处理异常，例如记录到日志
                catchEx.ShowBox("AppDomain.CurrentDomain.UnhandledException", ex);
            }
        }

        // 全局未处理异常处理程序（UI线程）
        private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            // 阻止应用程序崩溃
            e.Handled = true;

            catchEx.ShowBox("Application.Current.DispatcherUnhandledException", e.Exception);
        }

        //Task上未处理的异常
        private void TaskScheduler_UnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
        {
            // 标记异常已处理，防止应用程序崩溃
            e.SetObserved();

            catchEx.ShowBox("TaskScheduler.UnobservedTaskException", e.Exception);
        }
        #endregion

        #region 启用Aop
        internal void StartAop()
        {
            AopEvent<IVisualCamera>.Instance.AfterExecuted += VisualCamera_OnAopAfter;
            AopEvent<IVisualCamera>.Instance.ExceptionExecuted += VisualCamera_OnException;

            AopEvent<IPowerMeterBase>.Instance.AfterExecuted += PowerMeter_OnAopAfter;
            AopEvent<IPowerMeterBase>.Instance.ExceptionExecuted += PowerMeter_OnException;

            AopEvent<IElectricalBase>.Instance.AfterExecuted += Electrical_OnAopAfter;
            AopEvent<IElectricalBase>.Instance.ExceptionExecuted += Electrical_OnException;

            AopEvent<ITcpServer>.Instance.AfterExecuted += TcpServer_OnAopAfter;
            AopEvent<ITcpServer>.Instance.ExceptionExecuted += TcpServer_OnException;
            
        }

        private void TcpServer_OnException(ITcpServer server, MethodInfo info, Exception ex, double time, object[]? parObjs)
        {
            string msg = $"daMark execute [time: {time} ms] exception  method: {info.Name} [parameter: {parObjs?.ToJson()}] [exception: {ex.ToExString()}";
            logger.Error(msg);
        }

        private void TcpServer_OnAopAfter(ITcpServer server, MethodInfo info, object result, double time, object[]? parObjs)
        {
            logger.CameraVisual($"daMark execute [time: {time} ms] after  [method: {info.Name}] [result: {result?.ToJson()}] [parameter: {parObjs?.ToJson()}]");
        }

        #region 视觉相机
        private void VisualCamera_OnException(IVisualCamera camera, MethodInfo info, Exception ex, double time, object[]? parObjs)
        {
            string msg = $"daMark execute [time: {time} ms] exception [name: {camera.cameraName}] method: {info.Name} [parameter: {parObjs?.ToJson()}] [exception: {ex.ToExString()}";
            logger.CameraVisual(msg);
            logger.Error(msg);
        }

        private void VisualCamera_OnAopAfter(IVisualCamera camera, MethodInfo info, object result, double time, object[]? parObjs)
        {
            logger.CameraVisual($"daMark execute [time: {time} ms] after  [name: {camera.cameraName}] [method: {info.Name}] [result: {result?.ToJson()}] [parameter: {parObjs?.ToJson()}]");
        }
        #endregion

        #region 功率计
        private void PowerMeter_OnException(IPowerMeterBase powermete, MethodInfo info, Exception ex, double time, object[]? parObjs)
        {
            string msg = $"daMark execute [time: {time} ms] exception [name: {powermete.cameraName}] method: {info.Name} [parameter: {parObjs?.ToJson()}] [exception: {ex.ToExString()}";
            powerLog.Info(msg);
            logger.Error(msg);
        }

        private void PowerMeter_OnAopAfter(IPowerMeterBase powermete, MethodInfo info, object result, double time, object[]? parObjs)
        {
            powerLog.Info($"powerMeter execute [time: {time} ms] after  [name: {powermete.cameraName}] [method: {info.Name}] [result: {result?.ToJson()}] [parameter: {parObjs?.ToJson()}]");
        }
        #endregion

        #region 电源器
        private void Electrical_OnException(IElectricalBase electrical, MethodInfo info, Exception ex, double time, object[]? parObjs)
        {
            string msg = $"daMark execute [time: {time} ms] exception [name: {electrical.cameraName}] method: {info.Name} [parameter: {parObjs?.ToJson()}] [exception: {ex.ToExString()}";
            eleLog.Error(msg);
            logger.Error(msg);
        }

        private void Electrical_OnAopAfter(IElectricalBase electrical, MethodInfo info, object result, double time, object[]? parObjs)
        {
            eleLog.Info($"electrical execute [time: {time} ms] after  [name: {electrical.cameraName}] [method: {info.Name}] [result: {result?.ToJson()}]");
        }
        #endregion
        #endregion
    }
}
