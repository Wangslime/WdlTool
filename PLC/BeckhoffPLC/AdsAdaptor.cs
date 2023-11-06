using TwinCAT.Ads;

namespace BeckhoffPLC
{
    public class AdsAdaptor
    {

        public event Func<string, string> ReceiveEventMsg;
        public event Action<Exception> LogError;

        public AdsClient client;

        public bool Start(string netId, int port)
        {
            client ??= new AdsClient();
            client.Connect(netId, port);  // 替换 "NETID" 为实际的 Beckhoff PLC 的 NET ID

            client.AdsNotification += SubscriptionDataUpdated;
            return client.IsConnected;
        }

        private void SubscriptionDataUpdated(object? sender, AdsNotificationEventArgs e)
        {
            string value = BitConverter.ToString(e.Data.ToArray());
            string? result = ReceiveEventMsg?.Invoke(value);
            if (!string.IsNullOrEmpty(result)) 
            {
                
            }
        }

    }
}