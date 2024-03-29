using Drsoft.log.Nlog;

namespace TestMainClient
{
    internal class NlogTest
    {
        public static readonly string ConfigPath = "D:/ABCConfig";
        private string _drConfigPath = Path.Combine(ConfigPath, "EngineConfig.json");
        NLogger logger = NLogger.Instance;


        internal void Start()
        {
            string data = File.ReadAllText(_drConfigPath);
            data += data;
            data += data;
            data += data;
            data += data;
            for (int i = 0; i < 100; i++)
            {
                Task.Run(async () => 
                {
                    while (true) 
                    {
                        logger.Info(data);
                        await Task.Delay(10);
                    }
                });
            }



        }
    }
}
