using System.Collections.Concurrent;
using Newtonsoft.Json;

namespace Drsoft.log.Nlog
{
    internal class LogManager
    {
        public static Logger GetLogger(string name)
        {
            Logger loggers = new Logger();
            loggers.logger = NLog.LogManager.GetLogger(name);
            return loggers;
        }
    }

    internal class Logger
    {
        internal NLog.Logger logger = null;

        public void Trace(string log) => LogQueue.QueueActionEnqueue(() => logger?.Trace(log));
        public void Trace<T>(T t) => LogQueue.QueueActionEnqueue(() => Trace(ToJson(t)));
        public void Trace<T>(T t, string log) => LogQueue.QueueActionEnqueue(() => Trace(log + "," + ToJson(t)));
        public void Trace(Exception ex) => LogQueue.QueueActionEnqueue(() => logger?.Trace(ex));
        public void Trace(Exception ex, string log) => LogQueue.QueueActionEnqueue(() => logger?.Trace(ex, log));


        public void Debug(string log) => LogQueue.QueueActionEnqueue(() => logger?.Debug(log));
        public void Debug<T>(T t) => LogQueue.QueueActionEnqueue(() => Debug(ToJson(t)));
        public void Debug<T>(T t, string log) => LogQueue.QueueActionEnqueue(() => Debug(log + "," + ToJson(t)));
        public void Debug(Exception ex, string log) => LogQueue.QueueActionEnqueue(() => logger?.Debug(ex, log));
        public void Debug(Exception ex) => LogQueue.QueueActionEnqueue(() => logger?.Debug(ex));


        public void Info(string log) => LogQueue.QueueActionEnqueue(() => logger?.Info(log));
        public void Info<T>(T t) => LogQueue.QueueActionEnqueue(() => Info(ToJson(t)));
        public void Info<T>(T t, string log) => LogQueue.QueueActionEnqueue(() => Info(log + "," + ToJson(t)));
        public void Info(Exception ex, string log) => LogQueue.QueueActionEnqueue(() => logger?.Info(ex, log));
        public void Info(Exception ex) => LogQueue.QueueActionEnqueue(() => logger?.Info(ex));


        public void Warn(string log) => LogQueue.QueueActionEnqueue(() => logger?.Warn(log));
        public void Warn<T>(T t) => LogQueue.QueueActionEnqueue(() => Warn(ToJson(t)));
        public void Warn<T>(T t, string log) => LogQueue.QueueActionEnqueue(() => Warn(log + "," + ToJson(t)));
        public void Warn(Exception ex, string log) => LogQueue.QueueActionEnqueue(() => logger?.Warn(ex, log));
        public void Warn(Exception ex) => LogQueue.QueueActionEnqueue(() => logger?.Warn(ex));


        public void Error(string log) => LogQueue.QueueActionEnqueue(() => logger?.Error(log));
        public void Error<T>(T t) => LogQueue.QueueActionEnqueue(() => Error(ToJson(t)));
        public void Error<T>(T t, string log) => LogQueue.QueueActionEnqueue(() => Error(log + "," + ToJson(t)));
        public void Error(Exception ex, string log) => LogQueue.QueueActionEnqueue(() => logger?.Error(ex, log));
        public void Error(Exception ex) => LogQueue.QueueActionEnqueue(() => logger?.Error(ex));


        public void Fatal(string log) => LogQueue.QueueActionEnqueue(() => logger?.Fatal(log));
        public void Fatal<T>(T t) => LogQueue.QueueActionEnqueue(() => Fatal(ToJson(t)));
        public void Fatal<T>(T t, string log) => LogQueue.QueueActionEnqueue(() => Fatal(log + "," + ToJson(t)));
        public void Fatal(Exception ex, string log) => LogQueue.QueueActionEnqueue(() => logger?.Fatal(ex, log));
        public void Fatal(Exception ex) => LogQueue.QueueActionEnqueue(() => logger?.Fatal(ex));

        public void Close()
        {
            LogQueue.Instance.cts.Cancel();
            NLog.LogManager.Shutdown();
            LogQueue._instance = null;
        }

        private string ToJson<T>(T t)
        {
            return JsonConvert.SerializeObject(t);
        }
    }

    internal class LogQueue
    {
        #region 单例模式
        /// <summary>
        /// 单例模式对象锁
        /// </summary>
        private static readonly object lockObj = new object();
        internal static LogQueue _instance = null;

        /// <summary>
        /// 单例模式
        /// </summary>
        internal static LogQueue Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new LogQueue();
                        }
                    }
                }
                return _instance;
            }
        }

        internal static void QueueActionEnqueue(Action action)
        {
            Instance.QueueLogs.Enqueue(action);
            if (Instance.QueueLogs.Count > 1000)
            {
                while (Instance.QueueLogs.TryDequeue(out _)) { }
            }
        }

        #endregion

        internal CancellationTokenSource cts = new CancellationTokenSource();
        ~LogQueue()
        {
            cts.Cancel();
            NLog.LogManager.Shutdown();
            _instance = null;
        }
        LogQueue()
        {
            Task.Factory.StartNew(() => { TaskQueueLogs(); }, TaskCreationOptions.LongRunning);
        }
        private ConcurrentQueue<Action> QueueLogs = new ConcurrentQueue<Action>();

        private async void TaskQueueLogs()
        {
            while (!cts.Token.IsCancellationRequested)
            {
                try
                {
                    if (QueueLogs != null && QueueLogs.Any())
                    {
                        while (QueueLogs.TryDequeue(out Action action))
                        {
                            action?.Invoke();
                        }
                        GC.Collect();
                    }
                }
                catch (Exception)
                {
                    //LogManager.GetLogger("Error")?.Error(ex);
                }
                await Task.Delay(100, cts.Token);
            }
        }
    }
}