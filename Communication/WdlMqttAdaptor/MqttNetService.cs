using MQTTnet;
using MQTTnet.Protocol;
using MQTTnet.Server;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WdlMqttAdaptor
{
    public class MqttNetService
    {
        public event Action<string> LogEvent;
        public event Action<Exception> LogError;
        public event Func<string, string> ResivemsgEvent;

        MqttServer mqttService;
        public int servicePort;
        public string serviceTopic = "guideTopic";
        // <summary>
        /// 打开MQTT Server 服务
        /// </summary>
        /// <param name="port">端口号：默认为1883</param>
        public async Task StartMqttServer(CancellationToken token, int port = 10086)
        {
            servicePort = port;
            LogEvent?.Invoke("===启动客户端MQTT===");
            MqttServerOptionsBuilder optionsBuilder = new MqttServerOptionsBuilder();
            optionsBuilder.WithDefaultEndpoint();
            //optionsBuilder.WithDefaultEndpointBoundIPAddress(IPAddress.Parse("127.0.0.1"));
            optionsBuilder.WithDefaultEndpointPort(servicePort); // 设置 服务端 端口号
            //optionsBuilder.WithConnectionBacklog(1000); // 最大连接数
            MqttServerOptions options = optionsBuilder.Build();
            mqttService = new MqttFactory().CreateMqttServer(options);
            mqttService.ClientConnectedAsync += OnClientConnectedAsync; //客户端连接事件
            mqttService.ClientDisconnectedAsync += OnClientDisconnectedAsync; // 客户端关闭事件
            mqttService.ApplicationMessageNotConsumedAsync += OnApplicationMessageNotConsumedAsync; // 消息接收事件
            mqttService.ClientSubscribedTopicAsync += OnClientSubscribedTopicAsync; // 客户端订阅主题事件
            mqttService.ClientUnsubscribedTopicAsync += OnClientUnsubscribedTopicAsync; // 客户端取消订阅事件
            mqttService.StartedAsync += OnStartedAsync; // 启动后事件
            mqttService.StoppedAsync += OnStoppedAsync; // 关闭后事件
            //mqttService.InterceptingPublishAsync += OnInterceptingPublishAsync; // 消息接收事件
            //mqttService.ValidatingConnectionAsync += OnValidatingConnectionAsync; // 用户名和密码验证有关
            await mqttService.StartAsync();

            _ = Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    await MqttServerOpen(port);
                    await Task.Delay(5000);
                }
            }, token);
        }

        private async Task MqttServerOpen(int port)
        {
            if (mqttService == null || !mqttService.IsStarted)
            {
                LogEvent?.Invoke("===启动客户端MQTT===");
                MqttServerOptionsBuilder optionsBuilder = new MqttServerOptionsBuilder();
                optionsBuilder.WithDefaultEndpoint();
                optionsBuilder.WithDefaultEndpointPort(port); // 设置 服务端 端口号
                MqttServerOptions options = optionsBuilder.Build();
                mqttService = new MqttFactory().CreateMqttServer(options);
                mqttService.ClientConnectedAsync += OnClientConnectedAsync; //客户端连接事件
                mqttService.ClientDisconnectedAsync += OnClientDisconnectedAsync; // 客户端关闭事件
                mqttService.ApplicationMessageNotConsumedAsync += OnApplicationMessageNotConsumedAsync; // 消息接收事件
                mqttService.ClientSubscribedTopicAsync += OnClientSubscribedTopicAsync; // 客户端订阅主题事件
                mqttService.ClientUnsubscribedTopicAsync += OnClientUnsubscribedTopicAsync; // 客户端取消订阅事件
                mqttService.StartedAsync += OnStartedAsync; // 启动后事件
                mqttService.StoppedAsync += OnStoppedAsync; // 关闭后事件
                //mqttService.InterceptingPublishAsync += OnInterceptingPublishAsync; // 消息接收事件
                await mqttService.StartAsync();
            }
        }


        public async Task CloseMqttServer()
        {
            if (!mqttService.IsStarted)
                return;
            MqttServerOptionsBuilder serverOptions = new MqttServerOptionsBuilder();
            serverOptions.WithDefaultEndpointPort(servicePort);
            await mqttService.StopAsync();
        }

        /// <summary>
        /// 客户端订阅主题事件
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private Task OnClientSubscribedTopicAsync(ClientSubscribedTopicEventArgs arg)
        {
            return Task.Run(()=>
            {
                LogEvent?.Invoke($"ClientSubscribedTopicAsync：客户端ID=【{arg.ClientId}】订阅的主题=【{arg.TopicFilter}】 ");
            });
        }

        /// <summary>
        /// 关闭后事件
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private Task OnStoppedAsync(EventArgs arg)
        {
            return Task.Run(() =>
            {
                LogEvent?.Invoke($"StoppedAsync：MQTT服务已关闭……");
            });
        }

        /// <summary>
        /// 用户名和密码验证有关
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private Task OnValidatingConnectionAsync(ValidatingConnectionEventArgs arg)
        {
            return Task.Run(() =>
            {
                arg.ReasonCode = MqttConnectReasonCode.Success;
                if ((arg.Username ?? string.Empty) != "admin" || (arg.Password ?? String.Empty) != "123456")
                {
                    arg.ReasonCode = MqttConnectReasonCode.Banned;
                    LogEvent?.Invoke($"ValidatingConnectionAsync：客户端ID=【{arg.ClientId}】用户名或密码验证错误 ");
                }
            });
        }

        /// <summary>
        /// 启动后事件
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private Task OnStartedAsync(EventArgs arg)
        {
            return Task.Run(() =>
            {
                LogEvent?.Invoke($"StartedAsync：MQTT服务已启动……");
            });
        }

        /// <summary>
        /// 客户端取消订阅事件
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private Task OnClientUnsubscribedTopicAsync(ClientUnsubscribedTopicEventArgs arg)
        {
            return Task.Run(() =>
            {
                LogEvent?.Invoke($"ClientUnsubscribedTopicAsync：客户端ID=【{arg.ClientId}】已取消订阅的主题=【{arg.TopicFilter}】");
            });
        }

        /// <summary>
        /// 客户端断开时候触发
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private Task OnClientDisconnectedAsync(ClientDisconnectedEventArgs arg)
        {
            return Task.Run(() =>
            {
                LogEvent?.Invoke($"ClientDisconnectedAsync：客户端ID=【{arg.ClientId}】已断开, 地址=【{arg.Endpoint}】");
            });
        }

        /// <summary>
        /// 客户端连接时候触发
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private Task OnClientConnectedAsync(ClientConnectedEventArgs arg)
        {
            return Task.Run(() =>
            {
                LogEvent?.Invoke($"ClientConnectedAsync：客户端ID=【{arg.ClientId}】已连接, 用户名=【{arg.UserName}】地址=【{arg.Endpoint}】");
            });
        }

        /// <summary>
        /// 消息接收事件
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private Task OnInterceptingPublishAsync(InterceptingPublishEventArgs arg)
        {
            //if (string.Equals(arg.ClientId, ServerClientId))
            //{
            //    return Task.CompletedTask;
            //}
            //Console.WriteLine($"InterceptingPublishAsync：客户端ID=【{arg.ClientId}】 Topic主题=【{arg.ApplicationMessage.Topic}】 消息=【{Encoding.UTF8.GetString(arg.ApplicationMessage.Payload)}】 qos等级=【{arg.ApplicationMessage.QualityOfServiceLevel}】");
            //return Task.CompletedTask;
            return Task.Run(() => { });
        }

        private async Task OnApplicationMessageNotConsumedAsync(ApplicationMessageNotConsumedEventArgs arg)
        {
            try
            {
                string reciveMsg = Encoding.UTF8.GetString(arg.ApplicationMessage.Payload);
                LogEvent?.Invoke($"ApplicationMessageNotConsumedAsync：发送端ID=【{arg.SenderId}】 Topic主题=【{arg.ApplicationMessage.Topic}】 消息=【{reciveMsg}】 qos等级=【{arg.ApplicationMessage.QualityOfServiceLevel}】");
                reciveMsg = ResivemsgEvent?.Invoke(reciveMsg);
                if (!string.IsNullOrEmpty(reciveMsg))
                {
                    await PublishData(reciveMsg, arg.ApplicationMessage.Topic);
                }
            }
            catch (Exception ex)
            {
                LogError?.Invoke(ex);
            }
        }

        public Task PublishData(string data, string topic)
        {
            var mqttApplicationMessage = new MqttApplicationMessage
            {
                Topic = topic,
                Payload = Encoding.UTF8.GetBytes(data),
                QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                Retain = true  // 服务端是否保留消息。true为保留，如果有新的订阅者连接，就会立马收到该消息。
            };
            return mqttService.InjectApplicationMessage(new InjectedMqttApplicationMessage(mqttApplicationMessage) // 发送消息给有订阅 topic_01的客户端
            {
                SenderClientId = serviceTopic
            });
        }
    }
}
