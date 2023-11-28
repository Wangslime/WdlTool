using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ExtendPublic
{
    public class LogManager
    {
        public static Logger GetLogger(string name)
        {
            Logger loggers = new Logger();
            loggers.logger = NLog.LogManager.GetLogger(name);
            return loggers;
        }
    }

    public class Logger
    {
        internal NLog.Logger logger = null;

        public void Trace(string log) => LogQueue.QueueActionEnqueue(() => logger?.Trace(log));
        public void Trace<T>(T t) => LogQueue.QueueActionEnqueue(() => Trace(t.ToJson()));
        public void Trace<T>(T t, string log) => LogQueue.QueueActionEnqueue(() => Trace(log + "," + t.ToJson()));
        public void Trace(Exception ex) => LogQueue.QueueActionEnqueue(() => logger?.Trace(ex));
        public void Trace(Exception ex, string log) => LogQueue.QueueActionEnqueue(() => logger?.Trace(ex, log));


        public void Debug(string log) => LogQueue.QueueActionEnqueue(() => logger?.Debug(log));
        public void Debug<T>(T t) => LogQueue.QueueActionEnqueue(() => Debug(t.ToJson()));
        public void Debug<T>(T t, string log) => LogQueue.QueueActionEnqueue(() => Debug(log + "," + t.ToJson()));
        public void Debug(Exception ex, string log) => LogQueue.QueueActionEnqueue(() => logger?.Debug(ex, log));
        public void Debug(Exception ex) => LogQueue.QueueActionEnqueue(() => logger?.Debug(ex));


        public void Info(string log) => LogQueue.QueueActionEnqueue(() => logger?.Info(log));
        public void Info<T>(T t) => LogQueue.QueueActionEnqueue(() => Info(t.ToJson()));
        public void Info<T>(T t, string log) => LogQueue.QueueActionEnqueue(() => Info(log + "," + t.ToJson()));
        public void Info(Exception ex, string log) => LogQueue.QueueActionEnqueue(() => logger?.Info(ex, log));
        public void Info(Exception ex) => LogQueue.QueueActionEnqueue(() => logger?.Info(ex));


        public void Warn(string log) => LogQueue.QueueActionEnqueue(() => logger?.Warn(log));
        public void Warn<T>(T t) => LogQueue.QueueActionEnqueue(() => Warn(t.ToJson()));
        public void Warn<T>(T t, string log) => LogQueue.QueueActionEnqueue(() => Warn(log + "," + t.ToJson()));
        public void Warn(Exception ex, string log) => LogQueue.QueueActionEnqueue(() => logger?.Warn(ex, log));
        public void Warn(Exception ex) => LogQueue.QueueActionEnqueue(() => logger?.Warn(ex));


        public void Error(string log) => LogQueue.QueueActionEnqueue(() => logger?.Error(log));
        public void Error<T>(T t) => LogQueue.QueueActionEnqueue(() => Error(t.ToJson()));
        public void Error<T>(T t, string log) => LogQueue.QueueActionEnqueue(() => Error(log + "," + t.ToJson()));
        public void Error(Exception ex, string log) => LogQueue.QueueActionEnqueue(() => logger?.Error(ex, log));
        public void Error(Exception ex) => LogQueue.QueueActionEnqueue(() => logger?.Error(ex));


        public void Fatal(string log) => LogQueue.QueueActionEnqueue(() => logger?.Fatal(log));
        public void Fatal<T>(T t) => LogQueue.QueueActionEnqueue(() => Fatal(t.ToJson()));
        public void Fatal<T>(T t, string log) => LogQueue.QueueActionEnqueue(() => Fatal(log + "," + t.ToJson()));
        public void Fatal(Exception ex, string log) => LogQueue.QueueActionEnqueue(() => logger?.Fatal(ex, log));
        public void Fatal(Exception ex) => LogQueue.QueueActionEnqueue(() => logger?.Fatal(ex));

        public void Close()
        {
            LogQueue.Instance.cts.Cancel();
            NLog.LogManager.Shutdown();
            LogQueue._instance = null;
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
                await Task.Delay(500, cts.Token);
            }
        }
    }
}
