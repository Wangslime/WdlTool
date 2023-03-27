using Newtonsoft.Json;
using Test1;
using WdlEventBus;

namespace TestMain
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            new Test();
            while (true)
            {
                List<object> obj = await EventBus.PublishResultAsync("OnAddResultAsync");
                Console.WriteLine(JsonConvert.SerializeObject(obj.First()));
                Thread.Sleep(1000);
            }
        }
    }
}