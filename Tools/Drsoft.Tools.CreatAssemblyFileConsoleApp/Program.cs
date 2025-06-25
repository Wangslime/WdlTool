using Drsoft.Tools.DynamicBuilder;
using Newtonsoft.Json;

namespace Drsoft.Tools.CreatAssemblyFileConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string path = AppContext.BaseDirectory + "appconfig.json";
            string strConfig = File.ReadAllText(path);
            ConfigJsonInfo config = JsonConvert.DeserializeObject<ConfigJsonInfo>(strConfig);
            try
            {
                CreatAssemblyObj creatAssemblyObj = new CreatAssemblyObj();
                var ret = await creatAssemblyObj.StartCreatAssembly(config);
                if (ret.Item1)
                {
                    Console.WriteLine("Creat Assembly File Success!!!");
                }
                else
                {
                    Console.WriteLine("Creat Assembly File Fail!!!");
                }
                if (!string.IsNullOrEmpty(ret.Item2.Trim()))
                {
                    Console.WriteLine(ret.Item2);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message},{ex.StackTrace}");
            }
            Console.ReadLine();
        }
    }
}