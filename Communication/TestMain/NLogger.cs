using Newtonsoft.Json;
using System.Text;

namespace Drsoft.log.Nlog
{
    public class NLogger
    {
        #region 单例模式
        private static readonly object locked = new object();
        private static NLogger _Instance = new NLogger();
        public static NLogger Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (locked)
                    {
                        if (_Instance == null)
                        {
                            _Instance = new NLogger();
                        }
                    }
                }
                return _Instance;
            }
            set
            {
                lock (locked)
                {
                    _Instance = value;
                }
            }
        }

        private NLogger()
        {

        }
        #endregion

        private Logger trace = LogManager.GetLogger("Trace");
        private Logger debug = LogManager.GetLogger("Debug");
        private Logger info = LogManager.GetLogger("Info");
        private Logger error = LogManager.GetLogger("Error");
        private Logger fatal = LogManager.GetLogger("Fatal");

        private Logger dataBase = LogManager.GetLogger("DataBase");
        private Logger cameraVisual = LogManager.GetLogger("CameraVisual");
        private Logger drMark = LogManager.GetLogger("DrMark");
        private Logger plc = LogManager.GetLogger("PLC");

        private Logger com = LogManager.GetLogger("COM");
        private Logger business = LogManager.GetLogger("Business");
        private Logger ui = LogManager.GetLogger("UI");
        private Logger laser = LogManager.GetLogger("Laser");
        private Logger log = LogManager.GetLogger("Log");
        private Logger mes = LogManager.GetLogger("Mes");

        public void COM(string log)
        {
            com.Info(DateTimeNowStr(log));
        }
        public void Business(string log)
        {
            business.Info(DateTimeNowStr(log));
        }
        public void UI(string log)
        {
            ui.Info(DateTimeNowStr(log));
        }
        public void Laser(string log)
        {
            laser.Info(DateTimeNowStr(log));
        }
        public void Log(string log)
        {
            this.log.Info(DateTimeNowStr(log));
        }
        public void Mes(string log)
        {
            mes.Info(DateTimeNowStr(log));
        }
        public void DataBase(string log)
        {
            dataBase.Info(DateTimeNowStr(log));
        }
        public void DataBase<T>(T t)
        {
            dataBase.Info(DateTimeNowStr(JsonConvert.SerializeObject(t)));
        }
        public void DataBase<T>(string log, T t)
        {
            dataBase.Info(DateTimeNowStr(log) + "," + JsonConvert.SerializeObject(t));
        }
        public void CameraVisual(string log)
        {
            cameraVisual.Info(DateTimeNowStr(log));
        }
        public void DameraVisual<T>(T t)
        {
            cameraVisual.Info(DateTimeNowStr(JsonConvert.SerializeObject(t)));
        }
        public void DameraVisual<T>(string log, T t)
        {
            cameraVisual.Info(DateTimeNowStr(log) + "," + JsonConvert.SerializeObject(t));
        }
        public void DrMark(string log)
        {
            drMark.Info(DateTimeNowStr(log));
        }
        public void DrMark<T>(T t)
        {
            drMark.Info(DateTimeNowStr(JsonConvert.SerializeObject(t)));
        }
        public void DrMark<T>(string log, T t)
        {
            drMark.Info(DateTimeNowStr(log) + "," + JsonConvert.SerializeObject(t));
        }
        public void PLC(string log)
        {
            plc.Info(DateTimeNowStr(log));
        }
        public void PLC<T>(T t)
        {
            plc.Info(DateTimeNowStr(JsonConvert.SerializeObject(t)));
        }
        public void PLC<T>(string log, T t)
        {
            plc.Info(DateTimeNowStr(log) + "," + JsonConvert.SerializeObject(t));
        }



        public void Trace(string log)
        {
            trace.Trace(DateTimeNowStr(log));
        }
        public void Trace<T>(T t)
        {
            trace.Trace(DateTimeNowStr(JsonConvert.SerializeObject(t)));
        }
        public void Trace<T>(string log, T t)
        {
            trace.Trace(DateTimeNowStr(log) + "," + JsonConvert.SerializeObject(t));
        }
        public void Debug(string log)
        {
            debug.Debug(DateTimeNowStr(log));
        }
        public void Debug<T>(T t)
        {
            debug.Debug(DateTimeNowStr(JsonConvert.SerializeObject(t)));
        }
        public void Debug<T>(string log, T t)
        {
            debug.Debug(DateTimeNowStr(log) + "," + JsonConvert.SerializeObject(t));
        }
        public void Info(string log)
        {
            info.Info(DateTimeNowStr(log));
        }
        public void Info<T>(T t)
        {
            info.Info(DateTimeNowStr(JsonConvert.SerializeObject(t)));
        }
        public void Info<T>(string log, T t)
        {
            info.Info(DateTimeNowStr(log) + "," + JsonConvert.SerializeObject(t));
        }
        public void Error(string log)
        {
            error.Error(DateTimeNowStr(log));
        }
        public void Error<T>(T t)
        {
            error.Error(DateTimeNowStr(JsonConvert.SerializeObject(t)));
        }
        public void Error(Exception ex)
        {
            error.Error(ex, DateTimeNowStr());
        }
        public void Error<T>(string log, T t)
        {
            error.Error(DateTimeNowStr(log) + "," + JsonConvert.SerializeObject(t));
        }

        public void Error(Exception ex, string log)
        {
            error.Info(ex, DateTimeNowStr(log));
        }
        public void Fatal(string log)
        {
            fatal.Fatal(DateTimeNowStr(log));
        }
        public void Fatal<T>(T t)
        {
            fatal.Fatal(DateTimeNowStr(JsonConvert.SerializeObject(t)));
        }
        public void Fatal(Exception ex)
        {
            fatal.Fatal(ex, DateTimeNowStr());
        }
        public void Fatal<T>(string log, T t)
        {
            fatal.Fatal(DateTimeNowStr(log) + "," + JsonConvert.SerializeObject(t));
        }
        public void Fatal(Exception ex, string log)
        {
            fatal.Fatal(ex, DateTimeNowStr(log));
        }


        private string DateTimeNowStr(string log)
        {
            if (string.IsNullOrEmpty(log))
            {
                return "";
            }
            return $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} {log}";
        }
        private string DateTimeNowStr()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }

        public string ToExString(Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            string exMsg = $"[{ex.Message};{ex.StackTrace}]";
            sb.Append(exMsg);
            if (ex.InnerException != null)
            {
                sb.Append(ToExString(ex.InnerException));
            }
            return sb.ToString();
        }
    }
}