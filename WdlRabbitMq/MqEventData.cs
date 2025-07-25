﻿using System;

namespace Long.RabbitMq
{
    internal interface IMqEventData
    { 

    }

    public class MqEventData<T> : IMqEventData
    {
        public object Sender { get; set; }
        public DateTime EventTime { get; set; } = DateTime.Now;
        public string MethodName { get; set; } = "";
        public string EventName { get; set; } = "";
        public T Data { get; set; }
    }
}