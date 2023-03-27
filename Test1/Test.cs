using WdlEventBus;

namespace Test1
{
    public class Test
    {
        public Test()
        {
            //EventBus.Publish("OnAdd");
            EventBus.Subscribe(OnAdd);

            //EventBus.PublishResult("OnAddResult");
            EventBus.SubscribeResult(OnAddResult);

            //EventBus.PublishAsync("OnAddAsync");
            EventBus.SubscribeAsync(OnAddAsync);

            //EventBus.PublishResultAsync("OnAddResultAsync");
            EventBus.SubscribeResultAsync(OnAddResultAsync);


            //EventBus.Publish<string>("OnAdd");
            EventBus.Subscribe<string>(OnAdd);

            //EventBus.PublishResult<string>("OnAddResult");
            EventBus.SubscribeResult<string>(OnAddResult);

            //EventBus.PublishAsync<string>("OnAddAsync");
            EventBus.SubscribeAsync<string>(OnAddAsync);

            //EventBus.PublishResultAsync<string>("OnAddResultAsync");
            EventBus.SubscribeResultAsync<string>(OnAddResultAsync);
        }

        private void OnAdd(EventData<string> obj)
        {
            throw new NotImplementedException();
        }

        private object OnAddResult(EventData<string> arg)
        {
            throw new NotImplementedException();
        }

        private Task OnAddAsync(EventData<string> arg)
        {
            throw new NotImplementedException();
        }

        private Task<object> OnAddResultAsync(EventData<string> arg)
        {
            object result = arg;
            return Task.FromResult(result);
        }

        private Task<object> OnAddResultAsync(EventData arg)
        {
            object result = arg;
            return Task.FromResult(result);
        }

        private Task OnAddAsync(EventData arg)
        {
            throw new NotImplementedException();
        }

        private void OnAdd(EventData obj)
        {
            obj.MethodName = "hahaha";
            object result = obj;
        }
        private string OnAddResult(EventData obj)
        {
            obj.MethodName = "hahaha";
            return obj.MethodName;
        }
    }
}