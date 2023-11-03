using Confluent.Kafka;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WdlKafkaAdaptor
{
    public class KafkaConsumers
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
        /// 取消消费的事件
        /// </summary>
        private static CancellationTokenSource cancelToken = new CancellationTokenSource();
        /// <summary>
        /// 消费循环的委托
        /// </summary>
        public static Action<string> delProcessConsume = null;
        /// <summary>
        /// 日志委托
        /// </summary>
        public static Action<string> delLog = null;
        /// <summary>
        /// GUID，用于记录日志
        /// </summary>
        public static string ssid = "";

        /// <summary>
        /// 初始化消费者
        /// </summary>
        /// <param name="broker"></param>
        /// <param name="topicName"></param>
        public static void InitConsumer(string broker, string topicName, string consumerGroup, string guid)
        {
            ssid = guid;
            brokerList = broker;
            topic = topicName;
            groupId = consumerGroup;
        }

        /// <summary>
        /// 启动消费者
        /// </summary>
        public static void StartConsumer(string guid)
        {
            if (!string.IsNullOrEmpty(guid))
            {
                ssid = guid;
            }
            Task.Run(() =>
            {
                try
                {
                    string id = Process.GetCurrentProcess().Id.ToString() + "," + Thread.CurrentThread.ManagedThreadId.ToString();
                    delLog?.Invoke($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff")}(ID:{id}), SSID：{guid}，KafkaConsumer.Run_ManualAssign: 消费者线程开始轮询!");
                    Run_ManualAssign();
                }
                catch (Exception ex)
                {
                    delLog.Invoke(ex.Message + ex.StackTrace);
                }
            });
        }

        /// <summary>
        /// 停用消费者
        /// </summary>
        public static void StopConsumer(string guid)
        {
            string id = Process.GetCurrentProcess().Id.ToString() + "," + Thread.CurrentThread.ManagedThreadId.ToString();
            delLog?.Invoke($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff")}(ID:{id}), SSID：{ssid}，KafkaConsumer.StopConsumer: 消费者线程停止轮询!");
            ssid = guid;
            cancelToken.Cancel();
        }

        public static void Run_Consume1()
        {
            //首先构造一个强类型ConsumerConfig类的实例 ，然后将其传递给ConsumerBuilder的构造函数
            var conf = new ConsumerConfig
            {
                GroupId = "smartrtg",        //指定消费者是哪个消费者组
                BootstrapServers = "10.169.89.26:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                //指定在分区没有提交的偏移量或提交的偏移量无效（可能是由于日志截断）的情况下消费者应该开始读取的偏移量

                EnableAutoOffsetStore = false//<----this
            };
            using (var consumer = new ConsumerBuilder<Ignore, string>(conf)
                .SetErrorHandler((_, e) => Console.WriteLine($"Error: {e.Reason}"))
                .Build())
            {

                consumer.Subscribe("SmartRtg_Send");

                var cts = new CancellationTokenSource();
                Console.CancelKeyPress += (_, e) =>
                {
                    e.Cancel = true; // prevent the process from terminating.
                    cts.Cancel();
                };

                try
                {
                    while (true)
                    {
                        try
                        {
                            var consumeResult = consumer.Consume(cts.Token);
                            Console.WriteLine($"Received message at {consumeResult.TopicPartitionOffset}: ${consumeResult.Message.Value}");
                            consumer.StoreOffset(consumeResult);//<----this
                        }
                        catch (ConsumeException e)
                        {
                            Console.WriteLine($"Error occured: {e.Error.Reason}");
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    // Ensure the consumer leaves the group cleanly and final offsets are committed.
                    consumer.Close();
                }
            }
        }

        /// <summary>
        ///  消费者
        /// </summary>
        public static void Run_Consume()
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = brokerList,
                GroupId = groupId,
                EnableAutoCommit = false,    //禁止自动提交偏移
                //StatisticsIntervalMs = 5000,
                //SessionTimeoutMs = 6000,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnablePartitionEof = true
            };

            const int commitPeriod = 1;

            using (var consumer = new ConsumerBuilder<Ignore, string>(config)
                .SetErrorHandler((_, e) =>
                {
                    delLog?.Invoke($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff")}, KafkaConsumer SetErrorHandler: {e.Reason}");
                })
                .SetStatisticsHandler((_, json) =>
                {
                    delLog?.Invoke($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff")}, KafkaConsumer SetStatisticsHandler: {json.ToString()}");
                })
                .SetPartitionsAssignedHandler((c, partitions) =>  //消费者加入后，初次调用Consume()拉取消息之前被调用，用来保证消费者获取到Broker中正确的偏移量，即重置各消费者偏移量
                {
                    string msg = "KafkaConsumer SetPartitionsAssignedHandler: [" + string.Join(",", partitions.Select(p => p.Partition.Value)) +
                                 "], all: [" + string.Join(",", c.Assignment.Concat(partitions).Select(p => p.Partition.Value)) + "]";
                    delLog?.Invoke($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff")}, {msg}");
                })
                .SetPartitionsRevokedHandler((c, partitions) =>//消费者退出时，分区被取消，消费者可以向该分区提交自己当前的Offset，以避免数据重复消费
                {
                    var remaining = c.Assignment.Where(atp => partitions.Where(rtp => rtp.TopicPartition == atp).Count() == 0);
                    string msg = "KafkaConsumer SetPartitionsRevokedHandler: [" + string.Join(",", partitions.Select(p => p.Partition.Value)) +
                                 "], remaining: [" + string.Join(",", remaining.Select(p => p.Partition.Value)) + "]";
                    delLog?.Invoke($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff")}, {msg}");
                })
                .SetPartitionsLostHandler((c, partitions) =>
                {
                    string msg = $"KafkaConsumer SetPartitionsLostHandler: [{string.Join(", ", partitions)}]";
                    delLog?.Invoke($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff")}, {msg}");
                })
                .Build())
            {
                consumer.Subscribe(topic);  //订阅主题Topic
                try
                {
                    delLog?.Invoke($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff")}, KafkaConsumer.Run_Consume: 开始进入消费者轮询!");
                    //消费循环,一个典型的 Kafka 消费者应用程序以消费循环为中心，它重复调用Consume()以逐一检索消费者在后台线程中有效预取的记录。
                    while (true)
                    {
                        string msg = "";
                        try
                        {
                            var consumeResult = consumer.Consume(cancelToken.Token);
                            if (consumeResult.IsPartitionEOF)
                            {
                                msg = $"KafkaConsumer已读到主题的尾部({consumeResult.Topic}, Partition {consumeResult.Partition}, Offset {consumeResult.Offset})";
                                delLog?.Invoke($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff")}，{msg}");
                                continue;
                            }
                            msg = $"KafkaConsumer接收消息({consumeResult.TopicPartitionOffset}): {consumeResult.Message.Value}";
                            delLog?.Invoke($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff")}，{msg}");

                            //消息的偏移量，每隔5条报文就手动提交一次偏移
                            if (consumeResult.Offset % commitPeriod == 0)
                            {
                                try
                                {
                                    consumer.Commit(consumeResult); //手动提交偏移
                                    //Offset o = consumer.Position(consumeResult.TopicPartition); //获取下一次拉取消息的偏移量，当前偏移量+1
                                }
                                catch (KafkaException ex)
                                {
                                    delLog?.Invoke($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff")}，KafkaConsumer Commit Error: {ex.Error.Reason}");
                                }
                            }
                            delProcessConsume?.Invoke(consumeResult.Message.Value);
                        }
                        catch (ConsumeException ex)
                        {
                            delLog?.Invoke($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff")}，KafkaConsumer ConsumeException: {ex.Error.Reason}");
                        }
                        catch (OperationCanceledException ex)
                        {
                            delLog?.Invoke($"KafkaConsumer OperationCanceledException (关闭)：SSID：{ssid}，{ex.Message}，{ex.StackTrace}");
                            break;
                        }
                        catch (Exception ex)
                        {
                            delLog?.Invoke($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff")}，KafkaConsumer Exception Inner: {ex.Message}，{ex.StackTrace}");
                        }
                    }
                }
                catch (OperationCanceledException ex)
                {
                    delLog?.Invoke($"KafkaConsumer OperationCanceledException (关闭)：SSID：{ssid}，{ex.Message}，{ex.StackTrace}");
                }
                catch (Exception ex)
                {
                    delLog?.Invoke($"KafkaConsumer Exception Outer (关闭)：{ex.Message}，{ex.StackTrace}");
                }
                //发生异常后，确保消费者从消费者组中清除，触发组重新平衡，并提交最终偏移
                consumer.Close();
            }
        }

        /// <summary>
        /// 手动指定偏移消费
        /// </summary>
        public static void Run_ManualAssign()
        {
            string id = Process.GetCurrentProcess().Id.ToString() + "," + Thread.CurrentThread.ManagedThreadId.ToString();
            var config = new ConsumerConfig
            {
                GroupId = groupId,
                BootstrapServers = brokerList,
                EnableAutoCommit = true
            };

            using (var consumer = new ConsumerBuilder<Ignore, string>(config)
                .SetErrorHandler((_, e) =>
                {
                    delLog?.Invoke($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff")}(ID:{id}), KafkaConsumer SetErrorHandler: {e.Reason}");
                })
                .Build())
            {
                consumer.Assign(new TopicPartitionOffset(topic, 0, Offset.End));
                try
                {
                    while (true)
                    {
                        string msg = "";
                        try
                        {
                            var consumeResult = consumer.Consume(cancelToken.Token);
                            if (consumeResult.IsPartitionEOF)
                            {
                                msg = $"KafkaConsumer已读到主题的尾部({consumeResult.Topic},{consumeResult.Partition},{consumeResult.Offset})";
                                delLog?.Invoke($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff")}(ID:{id})，{msg}");
                                continue;
                            }
                            msg = $"KafkaConsumer接收消息({consumeResult.Topic},{consumeResult.Partition},{consumeResult.Offset}): {consumeResult.Message.Value}";
                            delLog?.Invoke($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff")}(ID:{id})，{msg}");
                            delProcessConsume?.Invoke(consumeResult.Message.Value);
                        }
                        catch (ConsumeException ex)
                        {
                            delLog?.Invoke($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff")}(ID:{id})，KafkaConsumer ConsumeException: {ex.Error.Reason}");
                        }
                        catch (OperationCanceledException ex)
                        {
                            delLog?.Invoke($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff")}(ID:{id})，KafkaConsumer OperationCanceledException(关闭)：SSID：{ssid}，{ex.Message}，{ex.StackTrace}");
                            break;
                        }
                        catch (Exception ex)
                        {
                            delLog?.Invoke($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff")}(ID:{id})，KafkaConsumer Exception Inner: {ex.Message}，{ex.StackTrace}");
                        }
                    }
                }
                catch (OperationCanceledException ex)
                {
                    delLog?.Invoke($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff")}(ID:{id})，KafkaConsumer OperationCanceledException(关闭)：SSID：{ssid}，{ex.Message}，{ex.StackTrace}");
                }
                catch (Exception ex)
                {
                    delLog?.Invoke($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff")}(ID:{id})，KafkaConsumer Exception Outer(关闭)：{ex.Message}，{ex.StackTrace}");
                }
                //发生异常后，确保消费者从消费者组中清除，触发组重新平衡，并提交最终偏移
                consumer.Close();
            }
        }
    }
}
