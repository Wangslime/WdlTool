using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Packets;
using MQTTnet.Protocol;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WdlMqttAdaptor
{
    public class MqttNetClient
    {
        /// <summary>
        /// 主题
        /// </summary>
        public string _ClientTopic = "Wdl";
        public string _ServerTopic = "";

        /// <summary>
        /// 客户端id
        /// </summary>
        public string _ClientId = Guid.NewGuid().ToString();

        /// <summary>
        /// 客户端连接服务端ip
        /// </summary>
        public string _IpAddress = "";

        /// <summary>
        /// 客户端连接服务端端口号
        /// </summary>
        public int _Port = 0;

        /// <summary>
        /// 用户
        /// </summary>
        public string _UserName = "";

        /// <summary>
        /// 用户
        /// </summary>
        public string _Password = "";

        // mqtt客户端
        private MqttClient mqttClient;
        //ClientId，同SessionId性质一样，是客户端的身份识别唯一标识

        public event Action<string> LogEvent;
        public event Action<Exception> LogError;
        public event Func<string, string> ResivemsgEvent;

        public async Task Start(CancellationToken token)
        {
            LogEvent?.Invoke("===启动客户端MQTT===");
            await ConnectMqttService();
            _ = Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    await ConnectMqttService();
                    await Task.Delay(5000);
                }
            }, token);

        }
        public async Task ConnectMqttService()
        {
            try
            {
                if (mqttClient == null || !mqttClient.IsConnected)
                {
                    LogEvent?.Invoke("MQTT初始化连接");
                    mqttClient = new MqttFactory()?.CreateMqttClient() as MqttClient;
                    //连接成功
                    mqttClient.ConnectedAsync += MqttClient_Connected;
                    //断开连接
                    mqttClient.DisconnectedAsync += MqttClient_Disconnected;
                    //订阅客户端或服务端发布的消息
                    mqttClient.ApplicationMessageReceivedAsync += MqttClient_ApplicationMessageReceived;
                    var options = new MqttClientOptions
                    {
                        ClientId = Guid.NewGuid().ToString(),
                        CleanSession = true,
                        ChannelOptions = new MqttClientTcpOptions
                        {
                            Server = _IpAddress,
                            Port = _Port
                        }
                    };
                    if (!string.IsNullOrEmpty(_UserName))
                    {
                        options.Credentials = new MqttClientCredentials(_UserName, UnicodeEncoding.UTF8.GetBytes(_Password));
                    }
                    await mqttClient.ConnectAsync(options);
                    MqttSubscribe();
                }
            }
            catch (Exception ex)
            {
                LogError?.Invoke(ex);
            }
        }

        //订阅接收消息事件
        private async Task MqttClient_ApplicationMessageReceived(MqttApplicationMessageReceivedEventArgs arg)
        {
            //订阅内容
            try
            {
                string Payload = Encoding.Default.GetString(arg.ApplicationMessage.Payload);
                LogEvent?.Invoke($"===订阅消息：{arg.ClientId}内容:{Payload}===");
                Payload = ResivemsgEvent?.Invoke(Payload);
                if (!string.IsNullOrEmpty(Payload))
                {
                    await MqttClientPublish(Payload);
                }
            }
            catch (Exception ex)
            {
                LogError?.Invoke(ex);
            }
        }


        //MQTT发布消息 content消息内容
        public async Task<bool> MqttClientPublish(string content, string topic = "")
        {
            try
            {
                if (string.IsNullOrEmpty(topic)) 
                {
                    topic = _ServerTopic;
                }
                var msg = new MqttApplicationMessage()
                {
                    Topic = topic,
                    Payload = Encoding.Default.GetBytes(content),
                    //QOS 消息等级
                    QualityOfServiceLevel = MqttQualityOfServiceLevel.AtMostOnce,
                    Retain = false
                };
                LogEvent?.Invoke($"===发送消息：{topic}内容:{content}===");
                MqttClientPublishResult mqttClientPublishResult = await mqttClient.PublishAsync(msg);
                if (mqttClientPublishResult.ReasonCode == MqttClientPublishReasonCode.Success)
                { 
                    return true;
                }
                else
                {
                    throw new Exception(mqttClientPublishResult.ReasonCode +","+ mqttClientPublishResult.ReasonString);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //订阅MQTT消息
        public void MqttSubscribe()
        {
            MqttClientSubscribeOptions options = new MqttClientSubscribeOptions();
            options.TopicFilters = new List<MqttTopicFilter>();
            MqttTopicFilter mqttTopicFilter = new MqttTopicFilter();
            mqttTopicFilter.Topic = _ClientTopic;
            mqttTopicFilter.QualityOfServiceLevel = MqttQualityOfServiceLevel.AtMostOnce;
            options.TopicFilters.Add(mqttTopicFilter);
            mqttClient?.SubscribeAsync(options);
        }

        //mqtt断开连接两种形式 长时间订阅未接收到消息和服务端断开连接
        private Task MqttClient_Disconnected(MqttClientDisconnectedEventArgs e)
        {
            Task task = ConnectMqttService();
            LogEvent?.Invoke($"====已断开MQTT服务器，断开时间{DateTime.Now}====");
            return task;
        }

        private Task MqttClient_Connected(MqttClientConnectedEventArgs arg)
        {
            return Task.Run(()=>
            {
                LogEvent?.Invoke($"=====连接MQTT服务器成功,连接时间{DateTime.Now}====");
            });
        }
    }
}