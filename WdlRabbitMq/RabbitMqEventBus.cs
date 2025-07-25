﻿using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Long.RabbitMq
{
    public class RabbitMqEventBus
    {
        #region 单例模式
        private static readonly object locked = new object();
        private static RabbitMqEventBus _Instance;
        public static RabbitMqEventBus Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (locked)
                    {
                        if (_Instance == null)
                        {
                            _Instance = new RabbitMqEventBus();
                        }
                    }
                }
                return _Instance;
            }
            set
            {
                lock (locked)
                {
                    _Instance = value;
                }
            }
        }

        private RabbitMqEventBus()
        { 
        
        }

        #endregion
        public event Action<string> OnLogEvent;
        public event Action<Exception> OnExceptionEvent;
        ConnectionFactory connectionFactory = new ConnectionFactory();
        IConnection connection = null;
        public bool IsConnect => connection?.IsOpen == true;

        public bool Start(string hostName, int port = 5672, string userName = "admin", string password = "123456" ,string virtualhost = "/")
        {
            connectionFactory = new ConnectionFactory();
            connectionFactory.HostName = hostName;
            connectionFactory.Port = port;
            connectionFactory.UserName = userName;
            connectionFactory.Password = password;
            connectionFactory.VirtualHost = virtualhost;
            connectionFactory.AutomaticRecoveryEnabled = true;  // 启用自动恢复
            connectionFactory.NetworkRecoveryInterval = TimeSpan.FromSeconds(10);
            connection = CreateConnection();
            return IsConnect;
        }

        public bool Start(RabbitMQConfig config)
        {
            ExchangeName = config.ExchangeName;
            MessageTTL = config.MessageTTL;
            PrefetchCount = config.PrefetchCount;
            return Start(config.HostName, config.Port, config.UserName, config.Password, config.VirtualHost);
        }

        public void Publish<T>(T t)
        {
            MqEventData<T> eventData = new MqEventData<T>();
            eventData.Data = t;
            Publish(eventData);
        }

        public void Publish<T>(T t, string eventName)
        {
            MqEventData<T> eventData = new MqEventData<T>();
            eventData.Data = t;
            eventData.EventName = eventName;
            Publish(eventData);
        }

        public void Publish<T>(MqEventData<T> data)
        {
            IModel channel = null;
            try
            {
                IConnection connection = CreateConnection();
                channel = connection.CreateModel();
                channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType.Direct);
                var props = channel.CreateBasicProperties();
                props.DeliveryMode = 2;
                var msg = JsonConvert.SerializeObject(data);
                var routingKey = typeof(T).Name;
                if (!string.IsNullOrEmpty(data.EventName))
                {
                    routingKey = data.EventName;
                }
                OnLogEvent?.Invoke($"消息发布：内容【{msg}】，exchange={ExchangeName}，routingKey={routingKey}");
                var body = Encoding.UTF8.GetBytes(msg);
                channel.ConfirmSelect();
                channel.BasicPublish(exchange: ExchangeName, routingKey: routingKey, basicProperties: props, body: body);
            }
            catch (Exception ex)
            {
                OnExceptionEvent?.Invoke(ex);
            }
            finally
            {
                channel?.Close();
                channel?.Dispose();
            }
        }

        private IConnection CreateConnection()
        {
            if (connection == null)
            {
                connection = connectionFactory.CreateConnection();
                AppDomain.CurrentDomain.ProcessExit += (sender, e) =>
                {
                    connection.Close();
                    connection?.Dispose();
                };
            }
            else if (!connection.IsOpen)
            {
                connection?.Close();
                connection?.Dispose();
                connection = connectionFactory.CreateConnection();
                AppDomain.CurrentDomain.ProcessExit += (sender, e) =>
                {
                    connection.Close();
                    connection?.Dispose();
                };
            }
            return connection;
        }

        public string ExchangeName = "IRabbitMqEventBus";
        public int MessageTTL = 5000;
        public ushort PrefetchCount = 1;

        public void Subscribe<T>(Action<MqEventData<T>> action, string eventName = "", bool isOpenDlx = false)
        {
            string queueName = "";
            var routingKey = typeof(T).Name;
            if (!string.IsNullOrEmpty(eventName))
            {
                routingKey = eventName;
            }
            var dlxExchange = $"{ExchangeName}.dlx";
            var arguments = new Dictionary<string, object>
            {
                { "x-dead-letter-exchange", dlxExchange },
                { "x-message-ttl", MessageTTL },
                { "x-dead-letter-routing-key", routingKey }
            };
            try
            {
                IConnection connection = CreateConnection();
                IModel channel = connection.CreateModel();
                channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType.Direct);
                channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: arguments);
                channel.QueueBind(queue: queueName, exchange: ExchangeName, routingKey: routingKey);
                channel.BasicQos(0, PrefetchCount, false);
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                    var eventData = JsonConvert.DeserializeObject<MqEventData<T>>(message);
                    action.Invoke(eventData);
                    channel.BasicAck(ea.DeliveryTag, false);
                    OnLogEvent?.Invoke($"消息订阅：内容【{message}】，exchange={ExchangeName}，routingKey={routingKey}");
                };
                channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
            }
            catch (Exception ex)
            {
                OnExceptionEvent?.Invoke(ex);
            }
            if (isOpenDlx)
            {
                SubscribeDlx(action);
            }
        }

        public void Subscribe<T>(Func<MqEventData<T>, bool> action, string eventName = "", bool isOpenDlx = false)
        {
            string queueName = action.Method.Name;
            var routingKey = typeof(T).Name;
            if (!string.IsNullOrEmpty(eventName))
            {
                routingKey = eventName;
            }
            var dlxExchange = $"{ExchangeName}.dlx";
            var arguments = new Dictionary<string, object>
            {
                { "x-dead-letter-exchange", dlxExchange },
                { "x-message-ttl", MessageTTL },
                { "x-dead-letter-routing-key", routingKey }
            };
            try
            {
                IConnection connection = CreateConnection();
                IModel channel = connection.CreateModel();
                channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType.Direct);
                channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: arguments);
                channel.QueueBind(queue: queueName, exchange: ExchangeName, routingKey: routingKey);
                channel.BasicQos(0, PrefetchCount, false);
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += async (model, ea) =>
                {
                    var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                    var eventMessage = JsonConvert.DeserializeObject<MqEventData<T>>(message);
                    MqEventData<T> eventData = new MqEventData<T>();
                    var result = action.Invoke(eventMessage);
                    if (result)
                    {
                        channel.BasicAck(ea.DeliveryTag, false);
                    }
                    else
                    {
                        channel.BasicNack(ea.DeliveryTag, false, true);
                    }
                    channel.BasicAck(ea.DeliveryTag, false);
                    string stResult = result ? "Success" : "Fail";
                    OnLogEvent?.Invoke($"消息订阅：内容【{message}】，exchange={ExchangeName}，routingKey={routingKey}，消息处理结果：{stResult}");
                };
                channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
            }
            catch (Exception ex)
            {
                OnExceptionEvent?.Invoke(ex);
            }
            if (isOpenDlx)
            {
                SubscribeDlx(action);
            }
        }

        private void SubscribeDlx<T>(Action<MqEventData<T>> action, string eventName = "")
        {
            string queueName = action.Method.Name;
            var routingKey = typeof(T).Name;
            if (!string.IsNullOrEmpty(eventName))
            {
                routingKey = eventName;
            }
            var exchangeName = $"{ExchangeName}.dlx";
            queueName += ".dlx";
            Dictionary<string, object> arguments = null;
            try
            {
                IConnection connection = CreateConnection();
                IModel channel = connection.CreateModel();
                channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType.Direct);
                channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: arguments);
                channel.QueueBind(queue: queueName, exchange: ExchangeName, routingKey: routingKey);
                channel.BasicQos(0, PrefetchCount, false);
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += async (model, ea) =>
                {
                    var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                    MqEventData<T> eventData = JsonConvert.DeserializeObject<MqEventData<T>>(message);
                    action.Invoke(eventData);
                    channel.BasicAck(ea.DeliveryTag, false);
                    channel.BasicAck(ea.DeliveryTag, false);
                    OnLogEvent?.Invoke($"消息订阅：内容【{message}】，exchange={ExchangeName}，routingKey={routingKey}");
                };
                channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
            }
            catch (Exception ex)
            {
                OnExceptionEvent?.Invoke(ex);
            }
        }

        private void SubscribeDlx<T>(Func<MqEventData<T>, bool> action, string eventName = "")
        {
            string queueName = action.Method.Name;
            var routingKey = typeof(T).Name;
            if (!string.IsNullOrEmpty(eventName))
            {
                routingKey = eventName;
            }
            var dlxExchange = $"{ExchangeName}.dlx";
            var arguments = new Dictionary<string, object>
            {
                { "x-dead-letter-exchange", dlxExchange },
                { "x-message-ttl", MessageTTL },
                { "x-dead-letter-routing-key", routingKey }
            };
            try
            {
                IConnection connection = CreateConnection();
                IModel channel = connection.CreateModel();
                channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType.Direct);
                channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: arguments);
                channel.QueueBind(queue: queueName, exchange: ExchangeName, routingKey: routingKey);
                channel.BasicQos(0, PrefetchCount, false);
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += async (model, ea) =>
                {
                    var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                    var eventData = JsonConvert.DeserializeObject<MqEventData<T>>(message);
                    var result = action.Invoke(eventData);
                    if (result)
                    {
                        channel.BasicAck(ea.DeliveryTag, false);
                    }
                    else
                    {
                        channel.BasicNack(ea.DeliveryTag, false, true);
                    }
                    channel.BasicAck(ea.DeliveryTag, false);
                    string stResult = result ? "Success" : "Fail";
                    OnLogEvent?.Invoke($"消息订阅：内容【{message}】，exchange={ExchangeName}，routingKey={routingKey}，消息处理结果：{stResult}");
                };
                channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
            }
            catch (Exception ex)
            {
                OnExceptionEvent?.Invoke(ex);
            }
        }

    }
}
