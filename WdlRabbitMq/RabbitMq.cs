using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;

namespace WdlRabbitMq
{
    public class RabbitMq
    {
        public event Func<string, string> ReceiveEventMsg;
        public string taskQueue = "task_queue";
        ConnectionFactory connectionFactory = new ConnectionFactory();

        public bool Start(string hostName, string userName, string password, string taskQueue = "")
        {
            connectionFactory = new ConnectionFactory();
            connectionFactory.HostName = hostName;
            connectionFactory.UserName = userName;
            connectionFactory.Password = password;
            if (!string.IsNullOrEmpty(taskQueue)) 
            {
                this.taskQueue = taskQueue;
            }
            Task.Run(()=>
            {
                while (true)
                {
                    Receive();
                }
            });
            return true;
        }

        public void Send(string data)
        {
            using (IConnection connection = connectionFactory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare(taskQueue, false, false, false, null);
                    channel.BasicPublish(string.Empty, taskQueue, null, Encoding.UTF8.GetBytes(data));
                }
            }
        }

        public void Receive()
        {
            using (IConnection connection = connectionFactory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare(taskQueue, false, false, false, null);
                    var consumer = new EventingBasicConsumer(channel);
                    BasicGetResult result = channel.BasicGet(taskQueue, true);
                    if (result != null)
                    {
                        string data = Encoding.UTF8.GetString(result.Body.ToArray());
                        data = ReceiveEventMsg?.Invoke(data);
                        if (!string.IsNullOrEmpty(data))
                        {
                            Send(data);
                        }
                    }
                }
            }
        }
    }
}
