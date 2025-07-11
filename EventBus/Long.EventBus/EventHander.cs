using System;
using System.Threading.Tasks;

namespace Long.EventBus
{
    internal class EventHander : IEventData
    { 
        internal Action<EventData> action { get; set; } = null;

        internal Func<EventData, object> func { get; set; } = null;

        internal EventHander(Action<EventData> action, Func<EventData, object> func)
        {
            this.action = action;
            this.func = func;
        }
        internal void HandleEventViod(EventData eventData)
        {
            action.Invoke(eventData);
        }

        internal object HandleEventObj(EventData eventData)
        {
            return func.Invoke(eventData);
        }
    }

    internal class EventHanderAsync : IEventData
    {
        internal Func<EventData, Task<object>> func { get; set; } = null;
        internal Func<EventData, Task> funcVoid { get; set; } = null;
        internal EventHanderAsync(Func<EventData, Task> funcVoid, Func<EventData, Task<object>> func)
        {
            this.funcVoid = funcVoid;
            this.func = func;
        }

        internal Task<object> HandleEventObj(EventData eventData)
        {
            return func.Invoke(eventData);
        }

        internal Task HandleEventVoid(EventData eventData)
        {
            return funcVoid.Invoke(eventData);
        }
    }

    internal class EventHander<T> : IEventData
    {
        internal Action<EventData<T>> action { get; set; } = null;

        internal Func<EventData<T>, object> func { get; set; } = null;

        internal EventHander(Action<EventData<T>> action, Func<EventData<T>, object> func)
        {
            this.action = action;
            this.func = func;
        }
        internal void HandleEventAction(EventData<T> eventData)
        {
            action.Invoke(eventData);
        }

        internal object HandleEventFunc(EventData<T> eventData)
        {
            return func.Invoke(eventData);
        }
    }

    internal class EventHanderAsync<T> : IEventData
    {
        internal Func<EventData<T>, Task<object>> func { get; set; } = null;
        internal Func<EventData<T>, Task> funcVoid { get; set; } = null;
        internal EventHanderAsync(Func<EventData<T>, Task> funcVoid, Func<EventData<T>, Task<object>> func)
        {
            this.funcVoid = funcVoid;
            this.func = func;
        }

        internal Task<object> HandleEventObj(EventData<T> eventData)
        {
            return func.Invoke(eventData);
        }

        internal Task HandleEventVoid(EventData<T> eventData)
        {
            return funcVoid.Invoke(eventData);
        }
    }
}