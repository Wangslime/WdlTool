using Confluent.Kafka;
using System.Diagnostics;

namespace CommonCommunication
{
    public class KafkaProducers
    {
        /// <summary>
        /// Broker服务器地址
        /// </summary>
        private static string brokerList = "";
        /// <summary>
        /// 主题
        /// </summary>
        private static string topic = "";
        /// <summary>
        /// 组名
        /// </summary>
        private static string groupId = "";
        /// <summary>
        /// 生产者消息发送出去的超时时间 (小于0，不设置属性，系统默认值为300000ms)
        /// </summary>
        private static string messageTimeoutMs = "-1";
        /// <summary>
        /// 锁
        /// </summary>
        private static readonly object locker = new object();
        /// <summary>
        /// 生产者
        /// </summary>
        private static IProducer<Null, string> producer = null;
        /// <summary>
        /// 日志委托
        /// </summary>
        public static Action<string> delLog = null;

        /// <summary>
        /// 生产者,单例模式
        /// </summary>
        public KafkaProducers()
        {
            if (producer == null)
            {
                lock (locker)
                {
                    if (producer == null)
                    {
                        var config = new ProducerConfig
                        {
                            BootstrapServers = brokerList
                        };
                        //发送出去的超时时间大于0，才能设置超时属性
                        if (int.TryParse(messageTimeoutMs, out int nMessageTimeOutMs) && nMessageTimeOutMs > 0)
                        {
                            config.MessageTimeoutMs = nMessageTimeOutMs;
                        }
                        producer = new ProducerBuilder<Null, string>(config).Build();
                    }
                }
            }
        }

        /// <summary>
        /// 初始化生产者
        /// </summary>
        public static void InitProducer(string broker, string topicName, string producerMessageTimeoutMs)
        {
            brokerList = broker;
            topic = topicName;
            messageTimeoutMs = producerMessageTimeoutMs;
        }

        /// <summary>
        /// 生产消息
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="message">需要传送的消息</param>
        public static bool Produce(string ssid, string msgSend, out string msgRet, string logPre = "", string topicNameNew = "")
        {
            msgRet = ""; bool bResult = true;
            //一般发送到默认的主题。当指定新的主题名称后，发送到新的主题
            string topicNameSend = "";
            if (!string.IsNullOrEmpty(topicNameNew))
            {
                topicNameSend = topicNameNew;
            }
            else
            {
                topicNameSend = topic;
            }
            new KafkaProducers();
            if (string.IsNullOrEmpty(msgSend))
            {
                msgRet = $"向Kafka发送消息报错：SSID：{ssid}，消息内容不能为空！";
                return false;
            }
            if (string.IsNullOrEmpty(topicNameSend))
            {
                msgRet = $"向Kafka发送消息报错：SSID：{ssid}，主题不能为空！";
                return false;
            }
            try
            {
                var deliveryReport = producer.ProduceAsync(topicNameSend, new Message<Null, string>
                {
                    Value = msgSend
                }).Result;
                if (deliveryReport.Status == PersistenceStatus.Persisted)
                {
                    msgRet = $"向Kafka发送消息成功({deliveryReport.Topic},{deliveryReport.Partition},{deliveryReport.Offset})：SSID：{ssid}，消息内容：{deliveryReport.Message.Value}";
                    bResult = true;
                }
                else
                {
                    msgRet = $"向Kafka发送消息报错({deliveryReport.Topic},{deliveryReport.Partition},{deliveryReport.Offset})：SSID：{ssid}，报错消息：{deliveryReport.Message.Value}";
                    bResult = false;
                }
            }
            catch (ProduceException<Null, string> ex)
            {
                msgRet = $"向Kafka发送消息报错：SSID：{ssid},消息内容：{msgSend}，错误消息：{ex.Message + " " + ex.InnerException?.ToString()}";
                bResult = false;
            }
            catch (Exception ex)
            {
                msgRet = $"向Kafka发送消息报错：SSID：{ssid},消息内容：{msgSend}，错误消息：{ex.Message + " " + ex.InnerException?.ToString()}";
                bResult = false;
            }
            if (!string.IsNullOrEmpty(msgRet))
            {
                logPre = string.IsNullOrEmpty(logPre) ? "" : $"({logPre})";
                delLog?.Invoke(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff") + $"{logPre} {msgRet}");
            }
            return bResult;
        }

        /// <summary>
        /// 生产消息
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="message">需要传送的消息</param>
        public static async Task<Tuple<bool, string>> ProduceAsync(string ssid, string msgSend, string logPre = "", string topicNameNew = "")
        {
            Tuple<bool, string> tuple;
            string msgRet;
            //一般发送到默认的主题。当指定新的主题名称后，发送到新的主题
            string topicNameSend = "";
            if (!string.IsNullOrEmpty(topicNameNew))
            {
                topicNameSend = topicNameNew;
            }
            else
            {
                topicNameSend = topic;
            }
            new KafkaProducers();
            if (string.IsNullOrEmpty(msgSend))
            {

                msgRet = $"向Kafka发送消息报错：SSID：{ssid}，消息内容不能为空！";
                tuple = Tuple.Create(false, msgRet);
                return tuple;
            }
            if (string.IsNullOrEmpty(topicNameSend))
            {
                msgRet = $"向Kafka发送消息报错：SSID：{ssid}，主题不能为空！";
                tuple = Tuple.Create(false, msgRet);
                return tuple;
            }
            try
            {
                var deliveryReport = await producer.ProduceAsync(topicNameSend, new Message<Null, string>
                {
                    Value = msgSend
                });
                if (deliveryReport.Status == PersistenceStatus.Persisted)
                {
                    msgRet = $"向Kafka发送消息成功({deliveryReport.Topic},{deliveryReport.Partition},{deliveryReport.Offset})：SSID：{ssid}，消息内容：{deliveryReport.Message.Value}";
                    tuple = Tuple.Create(true, msgRet);
                }
                else
                {
                    msgRet = $"向Kafka发送消息报错({deliveryReport.Topic},{deliveryReport.Partition},{deliveryReport.Offset})：SSID：{ssid}，报错消息：{deliveryReport.Message.Value}";
                    tuple = Tuple.Create(true, msgRet);
                }
            }
            catch (ProduceException<Null, string> ex)
            {
                msgRet = $"向Kafka发送消息报错：SSID：{ssid},消息内容：{msgSend}，错误消息：{ex.Message + " " + ex.InnerException?.ToString()}";
                tuple = Tuple.Create(false, msgRet);
            }
            catch (Exception ex)
            {
                msgRet = $"向Kafka发送消息报错：SSID：{ssid},消息内容：{msgSend}，错误消息：{ex.Message + " " + ex.InnerException?.ToString()}";
                tuple = Tuple.Create(false, msgRet);
            }
            if (!string.IsNullOrEmpty(msgRet))
            {
                logPre = string.IsNullOrEmpty(logPre) ? "" : $"({logPre})";
                delLog?.Invoke(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff") + $"{logPre} {msgRet}");
            }
            return tuple;
        }
    }
}
