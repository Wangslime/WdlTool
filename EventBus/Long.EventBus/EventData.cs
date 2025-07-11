using System;

namespace Long.EventBus
{
    internal interface IEventData
    { 

    }
    public class EventData : IEventData
    {
        public object Sender { get; set; }
        public DateTime EventTime { get; set; } = DateTime.Now;
        public string MethodName { get; set; } = "";
        public string EventName { get; set; } = "";
        public object Data { get; set; }
    }

    public class EventData<T> : IEventData
    {
        public object Sender { get; set; }
        public DateTime EventTime { get; set; } = DateTime.Now;
        public string MethodName { get; set; } = "";
        public string EventName { get; set; } = "";
        public T Data { get; set; }
    }
}